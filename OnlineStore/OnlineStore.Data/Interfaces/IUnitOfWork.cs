using OnlineStore.Data.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        public ICategoryRepository Categories { get; }
        public IOrderItemRepository OrderItems { get; }
        public IOrderRepository Orders { get; }
        public IProductRepository Products { get; }
        public Task<int> SaveChangesAsync();
    }
}
