using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Interfaces;
using OnlineStore.DTO.DTO;

namespace OnlineStore.WebApi.Controllers
{
    [ApiController]
    [Route("product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ??
                              throw new NullReferenceException(nameof(productService));
        }

        //[Authorize(Roles = "Admin")]
        [HttpPatch]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto productDto)
        {

            await _productService
                    .UpdateProductAsync(productDto, CancellationToken.None);

            return Ok(NoContent());
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {

            await _productService
                .DeleteProductByIdAsync(id, CancellationToken.None);


            return Ok(NoContent());
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateNewProduct([FromBody] ProductDto productDto)
        {
            await _productService
                    .CreateNewProductAsync(productDto, CancellationToken.None);

            return Ok(NoContent());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSelectedProduct([FromRoute] int id)
        {
            var categoryDto = await _productService
            .GetProductByIdAsync(id, CancellationToken.None);

            return Ok(categoryDto);

        }
    }
}
