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

        public async Task<ProductDto?> GetProductByIdAsync(Int32 productId)
        {
            if (productId < 1)
            {
                throw new ArgumentException(nameof(productId));
            }

            var product = await _unitOfWork.Products.GetByIdAsync(productId);

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<Boolean> DeleteProductByIdAsync(Int32 productId)
        {
            if (productId < 1)
            {
                throw new ArgumentException(nameof(productId));
            }

            var product = await _unitOfWork.Products.GetByIdAsync(productId);

            if (product == null)
            {
                return false;
            }

            await _unitOfWork.Products.Remove(productId);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<Int32> CreateNewProductAsync(ProductDto newProduct)
        {
            var product = await _unitOfWork.Products
                .FindBy(x => x.Name.Equals(newProduct.Name))
                .FirstOrDefaultAsync();

            if (product != null)
            {
                throw new InvalidOperationException($"Product {newProduct.Name} already exist");
            }


            var newProductToCreate = _mapper.Map<Product>(newProduct);


            await _unitOfWork.Products.AddAsync(newProductToCreate);

            await _unitOfWork.SaveChangesAsync();

            var createdProduct = await _unitOfWork.Products
                .FindBy(x => x.Name.Equals(newProduct.Name))
                .FirstOrDefaultAsync();

            if (newProduct.CategoriesId != null && newProduct.CategoriesId.Any())
            {
                await _unitOfWork.Products.AddCategoriesToProductAsync(createdProduct.Id, newProduct.CategoriesId);
            }

            return createdProduct!.Id;
        }

        public async Task<Boolean> UpdateProductAsync(ProductDto newProducts)
        {
            if (newProducts.Id < 1)
            {
                throw new ArgumentException(nameof(newProducts));
            }

            var product = await _unitOfWork.Products.GetByIdAsync(newProducts.Id);

            if (product == null)
            {
                return false;
            }

            var patchDtos = new List<Patch> {

                new Patch { PropertyName = "Name", PropertyValue = newProducts.Name },
                new Patch { PropertyName = "Description", PropertyValue = newProducts.Description},
                new Patch { PropertyName = "Price", PropertyValue = newProducts.Price }
            };

            await _unitOfWork.Products.PatchAsync(newProducts.Id, patchDtos);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ProductDto>?> GetProductsByPageAsync(Int32 page, Int32 pageSize)
        {
            if (page <= 0 || pageSize <= 0)
            {
                throw new ArgumentException($"attempt to use incorrect pagination arguments");
            }

            var products = await _unitOfWork.Products
                .GetProductsByPageAsync(page, pageSize); ;

            var productsDtoList = _mapper.Map<List<ProductDto>>(products);

            return productsDtoList;
        }

        public async Task<IEnumerable<ProductDto>?> GetProductsByCategoryAsync(Int32 categoryId)
        {

            if (categoryId >= 1)
            {
                var category = await _categoryService.GetCategoryByIdAsync(categoryId);

                if (category != null)
                {
                    var products = await _unitOfWork.Products.GetProductsByCategoryIdAsync(categoryId);

                    var productsDto = _mapper.Map<List<ProductDto>>(products);

                    return productsDto;
                }
            }
            return null;
        }
    }
}
