using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Models.Requests;
using OnlineStore.BusinessLogic.Models.Responses;
using OnlineStore.DTO.DTO;
using OnlineStore.WebApi.ServiceFactory;

namespace OnlineStore.WebApi.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly IServiceFactory _serviceFactory;

        public ProductsController(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory 
                              ?? throw new NullReferenceException(nameof(serviceFactory));
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsByPage([FromQuery] GetProductsByPageRequest request)
        {
            var productsByPageDto = _serviceFactory.CreateMapperService().Map<ProductsPaginationDto>(request);

            var productsList = await _serviceFactory
                .CreateProductService()
                .GetProductsByPageAsync(productsByPageDto, CancellationToken.None);

            return Ok(productsList);

        }

        [HttpGet("{categoryId:int}")]
        public async Task<IActionResult> GetProductsByCategory(Int32 categoryId)
        {
            if (categoryId < 1)
            {
                return BadRequest();
            }

            var productsByCategory = await _serviceFactory.CreateProductService()
                .GetProductsByCategoryAsync(categoryId, CancellationToken.None);

            return Ok(productsByCategory);
        }
    }
}
