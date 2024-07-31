using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Interfaces;
using OnlineStore.DTO.DTO;

namespace OnlineStore.WebApi.Controllers
{

    [ApiController]
    //[Authorize(Roles = "Admin")]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService ?? throw new NullReferenceException(nameof(orderService));
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersByPage([FromQuery] PaginationDto paginationDto)
        {
            var ordersDto = await _orderService
                .GetOrdersByPageAsync(paginationDto, CancellationToken.None);

            return Ok(ordersDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrdersByUserId([FromRoute] int id)
        {
            var ordersDto = await _orderService
                .GetOrdersByUserIdAsync(id, CancellationToken.None);

            return Ok(ordersDto);

        }
    }
}
