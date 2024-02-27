using OnlineStore.Data.Entities;

namespace OnlineStore.Data.Interfaces
{
    public interface ICategoryRepository : IRepository<Сategory>
    {
        public Task<IEnumerable<Сategory>?> GetCategoriesByPageAsync(int page, int pageSize
            ,CancellationToken cancellationToken);

    }
}
