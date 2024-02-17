using OnlineStore.Data.Entities;

namespace OnlineStore.Data.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        public Task<IEnumerable<Product>?> GetProductsByPageAsync(int page, int pageSize
            , CancellationToken cancellationToken);

        public Task<IEnumerable<Product>?> GetProductsByCategoryIdAsync(int categoryId
            , CancellationToken cancellationToken);

        public Task AddCategoriesToProductAsync(Product product, ICollection<int> categoriesId
            , CancellationToken cancellationToken);
        
    }
}
