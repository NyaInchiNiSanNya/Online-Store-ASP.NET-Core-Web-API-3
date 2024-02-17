using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Contexts;
using OnlineStore.Data.Entities;
using OnlineStore.Data.Interfaces;

namespace OnlineStore.Data.Repositories.Implementations
{
    internal class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ProductsOrdersContext productsOrdersContext)
            : base(productsOrdersContext)
        {

        }

        public async Task AddCategoriesToProductAsync(Product product, ICollection<int> categoriesId, CancellationToken cancellationToken)
        {
            var productToUpdate = await DbSet
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(x=>x.Name.Equals(product.Name), cancellationToken: cancellationToken);

            var categories = await Db.Categories.Where(c => categoriesId.Contains(c.Id))
                .ToListAsync(cancellationToken: cancellationToken);

            foreach (var category in categories)
            {
                productToUpdate.Categories.Add(category);
            }

            await Db.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>?> GetProductsByPageAsync(int page, int pageSize
            , CancellationToken cancellationToken)
        {
            var products = await DbSet
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken: cancellationToken);

            return products;
        }

        public async Task<IEnumerable<Product>?> GetProductsByCategoryIdAsync(int categoryId
            , CancellationToken cancellationToken)
        {
            var productsInCategory = await DbSet
                .AsNoTracking()
                .Where(p => p.Categories.Any(c => c.Id == categoryId))
                .ToListAsync(cancellationToken: cancellationToken);

            return productsInCategory;
        }

    }
}
