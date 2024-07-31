using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Interfaces;
using OnlineStore.DTO.DTO;

namespace OnlineStore.WebApi.Controllers
{
    [ApiController]
    [Route("order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService ?? throw new NullReferenceException(nameof(orderService));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] List<OrderItemDto> orderItems)
        {
            await _orderService
                .CreateNewOrderAsync(orderItems, CancellationToken.None);

            return Ok(NoContent());
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderById([FromRoute] int id)
        {
            var orderDto = await _orderService
                .GetOrderByIdAsync(id, CancellationToken.None);

            return Ok(orderDto);

        }
    }
}
