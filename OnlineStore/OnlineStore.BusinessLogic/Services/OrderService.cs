using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.BusinessLogic.Excpetions;
using OnlineStore.BusinessLogic.Interfaces;
using OnlineStore.Data.Entities;
using OnlineStore.Data.Interfaces;
using OnlineStore.DTO.DTO;

namespace OnlineStore.BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        private readonly IProductService _productService;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, 
            IIdentityService identityService, IProductService productService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public async Task CreateNewOrderAsync(ICollection<OrderItemDto> orderItemsDto
            , CancellationToken cancellationToken)
        {
            var userId = await _identityService.GetUserIdByNameAsync(cancellationToken);

            foreach (var orderItemDto in orderItemsDto)
            {
                if (!await _productService.DoesProductExistByIdAsync(orderItemDto.ProductId
                        , cancellationToken))
                {
                    throw new ObjectNotFoundException($"Product {orderItemDto.ProductId} not found");
                }
            }

            var order = new Order()
            {
                UserId = userId,
            };

            var ordersItem = _mapper.Map<ICollection<OrderItem>>(orderItemsDto);

            await _unitOfWork.Orders.CreateNewOrderAsync(order, ordersItem!, cancellationToken);

        }

        public async Task<OrdersDto> GetOrdersByPageAsync(PaginationDto paginationDto
            , CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.Orders
                .GetOrdersByPageAsync(paginationDto.Page, paginationDto.PageSize, cancellationToken);

            var ordersDtoList = _mapper.Map<List<OrderDto>>(orders);

            var totalOrdersCount = await _unitOfWork.Orders.CountAsync(cancellationToken);

            var ordersDto = new OrdersDto
            {
                Orders = ordersDtoList,
                TotalOrdersCount = totalOrdersCount
            };

            return ordersDto;
        }

        public async Task<OrderDto> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken)
        {
            if (orderId < 1)
            {
                throw new InvalidIdException($"Id {orderId} is invalid");
            }

            var order = await _unitOfWork.Orders
                .GetOrderByIdAsync(orderId, cancellationToken);

            if (order == null)
            {
                throw new ObjectNotFoundException($"Product {orderId} not found");
            }

            return _mapper.Map<OrderDto>(order)!;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(int userId, CancellationToken cancellationToken)
        {
            if (userId < 1)
            {
                throw new InvalidIdException($"Id {userId} is invalid");
            }

            var orders= await _unitOfWork.Orders.FindBy(order => order.UserId == userId)
                .ToListAsync(cancellationToken: cancellationToken);

            return _mapper.Map<List<OrderDto>>(orders)!; 
        }
    }
}
