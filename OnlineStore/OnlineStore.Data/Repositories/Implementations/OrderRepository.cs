using OnlineStore.Data.Contexts;
using OnlineStore.Data.Entities;
using OnlineStore.Data.Interfaces;

namespace OnlineStore.Data.Repositories.Implementations
{
    internal class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ProductsOrdersContext productsOrdersContext)
            : base(productsOrdersContext)
        {
        }

    }
}
