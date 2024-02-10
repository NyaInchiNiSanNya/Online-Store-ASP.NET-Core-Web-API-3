using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Models.Requests;
using OnlineStore.BusinessLogic.Models.Responses;
using OnlineStore.DTO.DTO;
using OnlineStore.WebApi.ServiceFactory;

namespace OnlineStore.WebApi.Controllers
{
    [ApiController]
    [Route("product")]
    public class ProductController : ControllerBase
    {
        private readonly IServiceFactory _serviceFactory;

        public ProductController(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory ?? throw new NullReferenceException(nameof(serviceFactory));
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch]
        public async Task<IActionResult> UpdateProduct([FromBody] PatchProductRequest request)
        {

            var newProduct = _serviceFactory.CreateMapperService().Map<ProductDto>(request);

            var isProductUpdated = await _serviceFactory
                .CreateProductService()
                .UpdateProductAsync(newProduct);

            if (isProductUpdated)
            {
                return Ok();
            }

            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(Int32 id)
        {
            if (id > 0)
            {
                var isProductDeleted = await _serviceFactory
                    .CreateProductService()
                    .DeleteProductByIdAsync(id);

                if (isProductDeleted)
                {
                    return Ok();
                }

                return NotFound();
            }


            return BadRequest();

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateNewProduct([FromBody] CreateNewProductRequest request)
        {
            var newProduct = _serviceFactory.CreateMapperService().Map<ProductDto>(request);

            Int32 productId;

            try
            {
                productId = await _serviceFactory
                    .CreateProductService()
                    .CreateNewProductAsync(newProduct);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(productId);
        }

        [Authorize(Roles = "User")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSelectedCategory(Int32 id)
        {
            if (id > 0)
            {
                ProductDto? categoryDto = await _serviceFactory
                    .CreateProductService()
                    .GetProductByIdAsync(id);

                if (categoryDto == null)
                {
                    return NotFound();
                }

                return Ok(_serviceFactory.CreateMapperService().Map<GetProductByIdResponse>(categoryDto));
            }

            return BadRequest();
        }
    }
}
