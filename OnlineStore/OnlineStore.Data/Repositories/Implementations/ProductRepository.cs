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

        public async Task AddCategoriesToProductAsync(Int32 productId, ICollection<Int32> categoriesId)
        {
            var product = await DbSet.Include(p => p.Categories)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                throw new ArgumentException($"Product with ID {productId} not found.");
            }

            var categories = await Db.Categories.Where(c => categoriesId.Contains(c.Id)).ToListAsync();

            if (categories.Count != categoriesId.Count)
            {
                throw new ArgumentException("One or more category IDs are invalid.");
            }

            foreach (var category in categories)
            {
                product.Categories.Add(category);
            }

            await Db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>?> GetProductsByPageAsync(Int32 page, Int32 pageSize)
        {
            var products = await DbSet
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<Product>?> GetProductsByCategoryIdAsync(Int32 categoryId)
        {
            var productsInCategory = await DbSet
                .AsNoTracking()
                .Where(p => p.Categories.Any(c => c.Id == categoryId))
                .ToListAsync();

            return productsInCategory;
        }

    }
}
