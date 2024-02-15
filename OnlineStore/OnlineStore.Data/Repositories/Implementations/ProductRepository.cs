using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task AddCategoriesToProductAsync(Int32 productId, ICollection<Int32> categoriesId, CancellationToken cancellationToken)
        {
            var product = await DbSet.FindAsync(productId);

            var categories = await Db.Categories.Where(c => categoriesId.Contains(c.Id))
                .ToListAsync(cancellationToken: cancellationToken);

            foreach (var category in categories)
            {
                product.Categories.Add(category);
            }

            await Db.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>?> GetProductsByPageAsync(Int32 page, Int32 pageSize
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

        public async Task<IEnumerable<Product>?> GetProductsByCategoryIdAsync(Int32 categoryId
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
