using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Models.Requests;
using OnlineStore.BusinessLogic.Models.Responses;
using OnlineStore.DTO.DTO;
using OnlineStore.WebApi.ServiceFactory;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.WebApi.Controllers
{
    [ApiController]
    [Route("categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly IServiceFactory _serviceFactory;

        public CategoriesController(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory ?? throw new NullReferenceException(nameof(serviceFactory));
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoriesByPage([FromQuery] GetCategoriesByPageRequest request)
        {
            var categoriesPagination = _serviceFactory.CreateMapperService().Map<CategoriesPaginationDto>(request);
            
            var categoriesList = await _serviceFactory
                .CreateCategoryService()
                .GetCategoriesByPageAsync(categoriesPagination, CancellationToken.None);

            return Ok(categoriesList);

        }
    }
}
