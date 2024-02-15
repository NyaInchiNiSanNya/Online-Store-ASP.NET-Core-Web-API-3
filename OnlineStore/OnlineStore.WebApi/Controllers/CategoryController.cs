using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Models.Requests;
using OnlineStore.BusinessLogic.Models.Responses;
using OnlineStore.DTO.DTO;
using OnlineStore.WebApi.ServiceFactory;

namespace OnlineStore.WebApi.Controllers
{
    [ApiController]
    [Route("category")]
    public class CategoryController : ControllerBase
    {
        private readonly IServiceFactory _serviceFactory;

        public CategoryController(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory ?? throw new NullReferenceException(nameof(serviceFactory));
        }

        //[Authorize(Roles = "Admin")]
        [HttpPatch]
        public async Task<IActionResult> UpdateCategory([FromBody] PatchCategoryRequest request)
        {

            var newCategory = _serviceFactory.CreateMapperService().Map<CategoryDto>(request);

            try
            {
                await _serviceFactory
                    .CreateCategoryService()
                    .UpdateCategoryAsync(newCategory, CancellationToken.None);

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
        public async Task<IActionResult> DeleteCategory(Int32 id)
        {
            if (id > 0)
            {
                try
                {
                    await _serviceFactory
                        .CreateCategoryService()
                        .DeleteCategoryByIdAsync(id, CancellationToken.None);
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
        public async Task<IActionResult> CreateNewCategory([FromBody] CreateNewCategoryRequest request)
        {
            var newCategory = _serviceFactory.CreateMapperService().Map<CategoryDto>(request);
            
            try
            {
                await _serviceFactory
                    .CreateCategoryService()
                    .CreateNewCategoryAsync(newCategory, CancellationToken.None);
            }
            catch(InvalidOperationException)
            {
                return BadRequest("category already exist");
            }

            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSelectedCategory(Int32 id)
        {
            if (id > 0)
            {
                try
                {
                    CategoryDto? categoryDto = await _serviceFactory
                        .CreateCategoryService()
                        .GetCategoryByIdAsync(id, CancellationToken.None);

                    return Ok(_serviceFactory.CreateMapperService().Map<GetCategoryByIdResponse>(categoryDto));
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
