using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Interfaces;
using OnlineStore.DTO.DTO;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.WebApi.Controllers
{
    [ApiController]
    [Route("categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new NullReferenceException(nameof(categoryService));
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoriesByPage([FromQuery] CategoriesPaginationDto paginationDto)
        {
            var categoriesList = await _categoryService
                .GetCategoriesByPageAsync(paginationDto, CancellationToken.None);

            return Ok(categoriesList);
        }
    }
}
