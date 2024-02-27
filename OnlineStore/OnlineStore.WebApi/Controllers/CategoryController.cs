using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Interfaces;
using OnlineStore.DTO.DTO;

namespace OnlineStore.WebApi.Controllers
{
    [ApiController]
    [Route("category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new NullReferenceException(nameof(categoryService));
        }

        //[Authorize(Roles = "Admin")]
        [HttpPatch]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDto categoryDto)
        {
            await _categoryService
                    .UpdateCategoryAsync(categoryDto, CancellationToken.None);

            return Ok(NoContent());
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            await _categoryService
                    .DeleteCategoryByIdAsync(id, CancellationToken.None);

            return Ok(NoContent());

        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateNewCategory([FromBody] CategoryDto categoryDto)
        {
            await _categoryService
                    .CreateNewCategoryAsync(categoryDto, CancellationToken.None);
                
            return Ok(NoContent());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSelectedCategory([FromRoute] int id)
        {
            var categoryDto = await _categoryService
                    .GetCategoryByIdAsync(id, CancellationToken.None);

            return Ok(categoryDto);
            
        }
    }
}
