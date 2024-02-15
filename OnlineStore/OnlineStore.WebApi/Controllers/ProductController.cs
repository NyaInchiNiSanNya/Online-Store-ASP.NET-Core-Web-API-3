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

        //[Authorize(Roles = "Admin")]
        [HttpPatch]
        public async Task<IActionResult> UpdateProduct([FromBody] PatchProductRequest request)
        {

            var newProduct = _serviceFactory.CreateMapperService().Map<ProductDto>(request);

            try
            {
                await _serviceFactory
                    .CreateProductService()
                    .UpdateProductAsync(newProduct, CancellationToken.None);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }

            return Ok();
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(Int32 id)
        {
            if (id > 0)
            {
                try
                {
                    await _serviceFactory
                        .CreateProductService()
                        .DeleteProductByIdAsync(id, CancellationToken.None);
                }
                catch (ArgumentException)
                {
                    return BadRequest();
                }
                catch (InvalidOperationException)
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateNewProduct([FromBody] CreateNewProductRequest request)
        {
            var newProduct = _serviceFactory.CreateMapperService().Map<ProductDto>(request);

            try
            {
                await _serviceFactory
                    .CreateProductService()
                    .CreateNewProductAsync(newProduct, CancellationToken.None);
            }
            catch (InvalidOperationException)
            {
                return BadRequest("product already exist");
            }

            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSelectedProduct(Int32 id)
        {
            if (id > 0)
            {
                try
                {
                    ProductDto? categoryDto = await _serviceFactory
                    .CreateProductService()
                    .GetProductByIdAsync(id, CancellationToken.None);

                    return Ok(_serviceFactory.CreateMapperService().Map<GetProductByIdResponse>(categoryDto));

                }
                catch (InvalidOperationException)
                {
                    return NotFound();
                }

            }
            return BadRequest();
        }
    }
}
