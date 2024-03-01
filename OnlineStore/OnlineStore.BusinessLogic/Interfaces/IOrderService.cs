using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.DTO.DTO;

namespace OnlineStore.BusinessLogic.Interfaces
{
    public interface IOrderService
    {
        public Task CreateNewOrderAsync(ICollection<OrderItemDto> orderItemsDto, CancellationToken cancellationToken);
        public Task<OrdersDto> GetOrdersByPageAsync(PaginationDto paginationDto, CancellationToken cancellationToken);
        public Task<OrderDto> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken);
        public Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(int userId, CancellationToken cancellationToken);
    }
}
