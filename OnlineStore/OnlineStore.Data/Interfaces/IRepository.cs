using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OnlineStore.DTO.Models;

namespace OnlineStore.Data.Interfaces
{
    public interface IRepository<T> : IDisposable
        where T : class, IBaseEntity
    {
        public Task<T?> GetByIdAsync(Int32 id);
        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        public IQueryable<T> GetAsQueryable();

        Task<EntityEntry<T>> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);

        Task PatchAsync(int id, List<Patch> patchDto);
        Task Update(T entity);

        Task Remove(int id);
        Task RemoveRange(IEnumerable<T> entities);

        Task<int> CountAsync();

    }
}
