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

        [Authorize(Roles = "Admin")]
        [HttpPatch]
        public async Task<IActionResult> UpdateCategory([FromBody] PatchCategoryRequest request)
        {

            var newCategory = _serviceFactory.CreateMapperService().Map<CategoryDto>(request);

            Boolean isCategoryUpdated;

            try
            {
                isCategoryUpdated = await _serviceFactory
                    .CreateCategoryService()
                    .UpdateCategoryAsync(newCategory);

            }
            catch (ArgumentException)
            {
                return BadRequest();
            }

            if (isCategoryUpdated)
            {
                return Ok();
            }

            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(Int32 id)
        {
            if (id > 0)
            {
                var isCategoryDeleted = await _serviceFactory
                    .CreateCategoryService()
                    .DeleteCategoryByIdAsync(id);

                if (isCategoryDeleted)
                {
                    return Ok();
                }

                return NotFound();
            }


            return BadRequest();

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateNewCategory([FromBody] CreateNewCategoryRequest request)
        {
            var newCategory = _serviceFactory.CreateMapperService().Map<CategoryDto>(request);

            Int32 categoryId;
            
            try
            {
                categoryId = await _serviceFactory
                    .CreateCategoryService()
                    .CreateNewCategoryAsync(newCategory);
            }
            catch(InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(categoryId);
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
    }
}
