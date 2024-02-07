using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Data.Contexts;
using OnlineStore.Data.Entities;
using OnlineStore.Data.Interfaces;

namespace OnlineStore.Data.Repositories.Implementations
{
    internal class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ProductsOrdersContext newsAggregatorContext)
            : base(newsAggregatorContext)
        {
        }

    }
}
