using AutoMapper;
using OnlineStore.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.BusinessLogic.Interfaces;
using OnlineStore.DTO.DTO;
using OnlineStore.DTO.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ProductDto?> GetProductByIdAsync(Int32 productId, CancellationToken cancellationToken)
        {
            if (productId < 1)
            {
                throw new ArgumentException(nameof(productId));
            }

            var product = await _unitOfWork.Products.GetByIdAsync(productId, cancellationToken);

            if (product == null)
            {
                throw new InvalidOperationException($"Product {productId} not found");
            }

            return _mapper.Map<ProductDto>(product);
        }

        public async Task DeleteProductByIdAsync(Int32 productId, CancellationToken cancellationToken)
        {
            if (productId < 1)
            {
                throw new ArgumentException(nameof(productId));
            }

            var product = await _unitOfWork.Products.GetByIdAsync(productId, cancellationToken);

            if (product == null)
            {
                throw new InvalidOperationException($"Product {productId} not found");
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
                throw new InvalidOperationException($"Product {newProduct.Name} already exist");
            }

            var newProductToCreate = _mapper.Map<Product>(newProduct);


            await _unitOfWork.Products.AddAsync(newProductToCreate, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var createdProduct = await _unitOfWork.Products
                .FindBy(x => x.Name.Equals(newProduct.Name))
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        }

        public async Task UpdateProductAsync(ProductDto newProducts, CancellationToken cancellationToken)
        {
            if (newProducts.Id < 1)
            {
                throw new ArgumentException(nameof(newProducts));
            }

            var product = await _unitOfWork.Products.GetByIdAsync(newProducts.Id, cancellationToken);

            if (product == null)
            {
                throw new InvalidOperationException($"Product {newProducts.Id} not found");
            }

            var patchDtos = new List<Patch> {

                new Patch { PropertyName = "Name", PropertyValue = newProducts.Name },
                new Patch { PropertyName = "Description", PropertyValue = newProducts.Description},
                new Patch { PropertyName = "Price", PropertyValue = newProducts.Price }
            };

            await _unitOfWork.Products.PatchAsync(newProducts.Id, patchDtos, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<ProductDto>?> GetProductsByPageAsync(ProductsPaginationDto paginationDto
            , CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.Products
                .GetProductsByPageAsync(paginationDto.Page, paginationDto.PageSize, cancellationToken); 

            var productsDtoList = _mapper.Map<List<ProductDto>>(products);

            return productsDtoList;
        }

        public async Task<IEnumerable<ProductDto>?> GetProductsByCategoryAsync(Int32 categoryId
            , CancellationToken cancellationToken)
        {

            if (categoryId >= 1)
            {
                var category = await _categoryService.GetCategoryByIdAsync(categoryId, cancellationToken);

                if (category != null)
                {
                    var products = await _unitOfWork.Products
                        .GetProductsByCategoryIdAsync(categoryId, cancellationToken);

                    var productsDto = _mapper.Map<List<ProductDto>>(products);

                    return productsDto;
                }
            }

            return null;
        }
    }
}
