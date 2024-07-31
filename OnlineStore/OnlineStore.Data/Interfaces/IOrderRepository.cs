using OnlineStore.Data.Entities;

namespace OnlineStore.Data.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        public Task CreateNewOrderAsync(Order order, IEnumerable<OrderItem> orderItems
            , CancellationToken cancellationToken);

        public Task<IEnumerable<Order>?> GetOrdersByPageAsync(int page, int pageSize
            , CancellationToken cancellationToken);

        public Task<Order?> GetOrderByIdAsync(int orderId
            , CancellationToken cancellationToken);
    }
}
