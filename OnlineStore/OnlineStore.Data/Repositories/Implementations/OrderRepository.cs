using Microsoft.EntityFrameworkCore;
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

        public async Task CreateNewOrderAsync(Order order, IEnumerable<OrderItem> orderItems
            , CancellationToken cancellationToken)
        {
            await DbSet.AddAsync(order, cancellationToken);

            await Db.SaveChangesAsync(cancellationToken);

            var orderId = order.Id;

            order.OrderItems = new List<OrderItem>();

            foreach (var orderItem in orderItems)
            {
                orderItem.OrderId = orderId;

                order.OrderItems!.Add(orderItem);
            }

            await Db.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Order>?> GetOrdersByPageAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            var orders = await DbSet
                .AsNoTracking()
                .Include(order => order.User)
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken: cancellationToken);

            return orders;
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken)
        {
            var order = await DbSet
                .AsNoTracking()
                .Where(order=>order.Id==orderId)
                .Include(order => order.User)
                .Include(order=>order.OrderItems)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            return order;
        }
    }
}
