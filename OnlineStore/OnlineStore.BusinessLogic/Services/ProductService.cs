using AutoMapper;
using OnlineStore.Data.Interfaces;
using OnlineStore.BusinessLogic.Interfaces;
using OnlineStore.DTO.DTO;
using OnlineStore.DTO.Models;
using Microsoft.EntityFrameworkCore;
using OnlineStore.BusinessLogic.Excpetions;
using OnlineStore.Data.Entities;

namespace OnlineStore.BusinessLogic.Services
{
    internal class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public ProductService(IUnitOfWork unitOfWork,
            IMapper mapper, ICategoryService categoryService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));

        }

        public async Task<ProductDto?> GetProductByIdAsync(int productId, CancellationToken cancellationToken)
        {
            if (productId < 1)
            {
                throw new InvalidIdException($"Id {productId} is invalid");
            }

            var product = await _unitOfWork.Products.GetByIdAsync(productId, cancellationToken);

            if (product == null)
            {
                throw new ObjectNotFoundException($"Product {productId} not found");
            }

            return _mapper.Map<ProductDto>(product);
        }

        public async Task DeleteProductByIdAsync(int productId, CancellationToken cancellationToken)
        {
            if (productId < 1)
            {
                throw new InvalidIdException($"Id {productId} is invalid");
            }

            var product = await _unitOfWork.Products.GetByIdAsync(productId, cancellationToken);

            if (product == null)
            {
                throw new ObjectNotFoundException($"Product {productId} not found");
            }

            await _unitOfWork.Products.RemoveAsync(productId, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task CreateNewProductAsync(ProductDto newProduct, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products
                .FindBy(x => x.Name.Equals(newProduct.Name))
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (product != null)
            {
                throw new ObjectAlreadyExistException($"Product {newProduct.Name} already exist");
            }

            var newProductToCreate = _mapper.Map<Product>(newProduct);

            await _unitOfWork.Products.AddAsync(newProductToCreate!, cancellationToken);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (newProduct.CategoriesId != null && newProduct.CategoriesId.Any())
            {
                await _unitOfWork.Products.AddCategoriesToProductAsync(newProductToCreate!
                    , newProduct.CategoriesId, cancellationToken);
            }

        }

        public async Task UpdateProductAsync(ProductDto newProduct, CancellationToken cancellationToken)
        {
            if (newProduct.Id < 1)
            {
                throw new InvalidIdException($"Id {newProduct.Id} is invalid");
            }

            var product = await _unitOfWork.Products.GetByIdAsync(newProduct.Id, cancellationToken);

            if (product == null)
            {
                throw new ObjectNotFoundException($"Product {newProduct.Id} not found");
            }

            var patchDto = new List<Patch> {

                new Patch { PropertyName = "Name", PropertyValue = newProduct.Name },
                new Patch { PropertyName = "Description", PropertyValue = newProduct.Description},
                new Patch { PropertyName = "Price", PropertyValue = newProduct.Price }
            };

            await _unitOfWork.Products.PatchAsync(newProduct.Id, patchDto, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<ProductsDto> GetProductsByPageAsync(PaginationDto paginationDto
            , CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.Products
                .GetProductsByPageAsync(paginationDto.Page, paginationDto.PageSize, cancellationToken);
            
            var productsDtoList = _mapper.Map<List<ProductDto>>(products);
            
            var totalProductsCount = await _unitOfWork.Products.CountAsync(cancellationToken);

            var productsDto = new ProductsDto()
            {
                Products = productsDtoList,
                TotalProductsCount = totalProductsCount
            };

            return productsDto;
        }

        public async Task<bool> DoesProductExistByIdAsync(int productId
            , CancellationToken cancellationToken)
        {
            if (productId < 1)
            {
                throw new InvalidIdException($"Id {productId} is invalid");
            }

            if ( await _unitOfWork.Products.GetByIdAsync(productId, cancellationToken) == null)
            {
                throw new ObjectNotFoundException($"Product {productId} not found");
            }

            return true;
        }

        public async Task<IEnumerable<ProductDto>?> GetProductsByCategoryAsync(int categoryId
            , CancellationToken cancellationToken)
        {
            if (categoryId < 1)
            {
                throw new InvalidIdException($"Id {categoryId} is invalid");
            }

            var category = await _categoryService.GetCategoryByIdAsync(categoryId, cancellationToken);

            if (category == null)
            {
                throw new ObjectNotFoundException($"Category {categoryId} not found");
            }


            var products = await _unitOfWork.Products
                .GetProductsByCategoryIdAsync(categoryId, cancellationToken);

            var productsDto = _mapper.Map<List<ProductDto>>(products);

            return productsDto;
        }
    }
}
