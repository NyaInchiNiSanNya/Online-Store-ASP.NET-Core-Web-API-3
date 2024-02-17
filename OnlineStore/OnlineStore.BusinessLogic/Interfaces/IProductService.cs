using OnlineStore.DTO.DTO;

namespace OnlineStore.BusinessLogic.Interfaces
{
    public interface IProductService
    {
        public Task<ProductDto?> GetProductByIdAsync(int categoryId, CancellationToken cancellationToken);

        public Task DeleteProductByIdAsync(int categoryId, CancellationToken cancellationToken);

        public Task CreateNewProductAsync(ProductDto newCategory, CancellationToken cancellationToken);

        public Task UpdateProductAsync(ProductDto newProduct, CancellationToken cancellationToken);

        public Task<IEnumerable<ProductDto>?> GetProductsByPageAsync(ProductsPaginationDto productsPagination
            , CancellationToken cancellationToken);

        public Task<IEnumerable<ProductDto>?> GetProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken);
    }
}
