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

        public async Task<IEnumerable<Сategory>?> GetCategoriesByPageAsync(int page, int pageSize
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
