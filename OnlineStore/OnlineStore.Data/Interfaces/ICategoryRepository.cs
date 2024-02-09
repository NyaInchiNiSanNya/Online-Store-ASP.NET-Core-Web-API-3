using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Data.Entities;

namespace OnlineStore.Data.Interfaces
{
    public interface ICategoryRepository : IRepository<Сategory>
    {
        public Task<IEnumerable<Сategory>?> GetCategoriesByPageAsync(Int32 page, Int32 pageSize);

    }
}
