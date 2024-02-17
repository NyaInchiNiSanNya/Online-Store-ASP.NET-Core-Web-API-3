using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Data.Contexts;
using OnlineStore.Data.Interfaces;
using OnlineStore.DTO;
using OnlineStore.DTO.Models;

namespace OnlineStore.Data.Repositories.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class, IBaseEntity
    {
        protected readonly ProductsOrdersContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(ProductsOrdersContext productsOrdersContext)
        {
            Db = productsOrdersContext;
            DbSet = Db.Set<TEntity>();
        }


        public virtual void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        public virtual async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(entity => entity.Id == id
                , cancellationToken: cancellationToken);
        }

        public virtual IQueryable<TEntity> FindBy(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var result = DbSet.Where(predicate);
            if (includes.Any())
            {
                result = includes
                    .Aggregate(result,
                        (current, include)
                            => current.Include(include));
            }
            return result;
        }

        public virtual IQueryable<TEntity> GetAsQueryable()
        {
            return DbSet;
        }

        public virtual async Task<EntityEntry<TEntity>> AddAsync(TEntity entity
            , CancellationToken cancellationToken)
        {
            return await DbSet.AddAsync(entity, cancellationToken);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            await DbSet.AddRangeAsync(entities, cancellationToken);
        }

        public virtual async Task PatchAsync(int id, List<Patch> patchDto, CancellationToken cancellationToken)
        {
            var entity =
                await DbSet.FirstOrDefaultAsync(ent => ent.Id == id, cancellationToken: cancellationToken);

            var nameValuePairProperties = patchDto.ToDictionary
            (k => k.PropertyName,
                v => v.PropertyValue);

            var dbEntityEntry = Db.Entry(entity);
            dbEntityEntry.CurrentValues.SetValues(nameValuePairProperties);
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            DbSet.Update(entity);
        }

        public virtual async Task RemoveAsync(int id, CancellationToken cancellationToken)
        {
            var entity =
                await DbSet.FirstOrDefaultAsync(ent => ent.Id == id, cancellationToken: cancellationToken);
            if (entity != null) DbSet.Remove(entity);
        }

        public virtual async Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            DbSet.RemoveRange(entities);
        }

        public virtual async Task<int> CountAsync(CancellationToken cancellationToken)
        {
            return await DbSet.CountAsync(cancellationToken: cancellationToken);
        }
    }
}
