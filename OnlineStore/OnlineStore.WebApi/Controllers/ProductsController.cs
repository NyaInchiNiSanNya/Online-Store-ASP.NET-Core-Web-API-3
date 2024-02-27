using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Interfaces;
using OnlineStore.DTO.DTO;

namespace OnlineStore.WebApi.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService
                              ?? throw new NullReferenceException(nameof(productService));
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsByPage([FromQuery] ProductsPaginationDto productsByPageDto)
        {
            var productsList = await _productService
                .GetProductsByPageAsync(productsByPageDto, CancellationToken.None);

            return Ok(productsList);

        }

        [HttpGet("{categoryId:int}")]
        public async Task<IActionResult> GetProductsByCategory([FromRoute] int categoryId)
        {
            var productsByCategory = await _productService
                .GetProductsByCategoryAsync(categoryId, CancellationToken.None);

            return Ok(productsByCategory);
        }
    }
}
