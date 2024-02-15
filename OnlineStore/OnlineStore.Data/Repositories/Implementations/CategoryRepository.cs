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
    internal class CategoryRepository : Repository<Сategory>, ICategoryRepository
    {
        public CategoryRepository(ProductsOrdersContext productsOrdersContext)
            : base(productsOrdersContext)
        {
            
        }

        public async Task<IEnumerable<Сategory>?> GetCategoriesByPageAsync(Int32 page, Int32 pageSize
            , CancellationToken cancellationToken)
        {
            var categories = await DbSet
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken: cancellationToken);

            return categories;
        }

    }
}
