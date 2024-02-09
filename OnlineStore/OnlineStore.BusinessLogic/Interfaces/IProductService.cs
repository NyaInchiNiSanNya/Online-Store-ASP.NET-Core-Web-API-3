using OnlineStore.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Interfaces
{
    public interface IProductService
    {
        public Task<ProductDto?> GetProductByIdAsync(Int32 categoryId);

        public Task<Boolean> DeleteProductByIdAsync(Int32 categoryId);

        public Task<Int32> CreateNewProductAsync(ProductDto newCategory);

        public Task<Boolean> UpdateProductAsync(ProductDto newCategory);

        public Task<IEnumerable<ProductDto>?> GetProductsByPageAsync(Int32 page, Int32 pageSize);

        public Task<IEnumerable<ProductDto>?> GetProductsByCategoryAsync(Int32 categoryId);
    }
}
