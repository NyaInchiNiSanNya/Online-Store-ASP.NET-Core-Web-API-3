using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Data.Entities;

namespace OnlineStore.Data.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        public Task<IEnumerable<Product>?> GetProductsByPageAsync(Int32 page, Int32 pageSize);

        public Task<IEnumerable<Product>?> GetProductsByCategoryIdAsync(Int32 categoryId);

        public Task AddCategoriesToProductAsync(Int32 productId, ICollection<Int32> categoriesId);
    }
}
