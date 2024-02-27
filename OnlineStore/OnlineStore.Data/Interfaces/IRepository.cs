using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OnlineStore.DTO.Models;

namespace OnlineStore.Data.Interfaces
{
    public interface IRepository<T> : IDisposable
        where T : class, IBaseEntity
    {
        public Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
        
        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate
            , params Expression<Func<T, object>>[] includes);
        
        public IQueryable<T> GetAsQueryable();

        Task<EntityEntry<T>> AddAsync(T entity, CancellationToken cancellationToken);
        
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);

        Task PatchAsync(int id, List<Patch> patchDto, CancellationToken cancellationToken);
        
        Task UpdateAsync(T entity, CancellationToken cancellationToken);

        Task RemoveAsync(int id, CancellationToken cancellationToken);
        
        Task RemoveRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);

        Task<int> CountAsync(CancellationToken cancellationToken);

    }
}
