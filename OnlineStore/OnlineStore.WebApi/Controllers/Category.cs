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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSelectedCategory(Int32 id)
        {
            if (id > 0)
            {
                CategoryDto? categoryDto = await _serviceFactory
                    .CreateCategoryService()
                    .GetCategoryByIdAsync(id);

                if (categoryDto == null)
                {
                    return NotFound();
                }

                return Ok(_serviceFactory.CreateMapperService().Map<GetCategoryByIdResponse>(categoryDto));
            }

            return BadRequest();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateCategory([FromBody] PatchCategoryRequest request)
        {

            var newCategory = _serviceFactory.CreateMapperService().Map<CategoryDto>(request);

            var isCategoryUpdate = await _serviceFactory
                .CreateCategoryService()
                .UpdateCategoryAsync(newCategory);

            if (isCategoryUpdate)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(Int32 id)
        {
            if (id > 0)
            {
                var isCategoryDelete = await _serviceFactory
                    .CreateCategoryService()
                    .DeleteCategoryByIdAsync(id);

                if (isCategoryDelete)
                {
                    return Ok();
                }

                return NotFound();
            }


            return BadRequest();

        }

        [HttpPost]
        public async Task<IActionResult> CreateNewCategory([FromBody] CreateNewCategoryRequest request)
        {
            var newCategory = _serviceFactory.CreateMapperService().Map<CategoryDto>(request);

            var isCategoryCreate = await _serviceFactory
                .CreateCategoryService()
                .CreateNewCategoryAsync(newCategory);

            if (isCategoryCreate)
            {
                return Ok();
            }

            return BadRequest();
        }

    }
}
