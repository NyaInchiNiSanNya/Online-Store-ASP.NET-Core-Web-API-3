using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.DTO.DTO;

namespace OnlineStore.BusinessLogic.Interfaces
{
    public interface ICategoryService
    {
        public Task<CategoryDto?> GetCategoryByIdAsync(Int32 categoryId, CancellationToken cancellationToken);

        public Task DeleteCategoryByIdAsync(Int32 categoryId, CancellationToken cancellationToken);

        public Task CreateNewCategoryAsync(CategoryDto newCategory, CancellationToken cancellationToken);

        public Task UpdateCategoryAsync(CategoryDto newCategory, CancellationToken cancellationToken);

        public Task<IEnumerable<CategoryDto>?> GetCategoriesByPageAsync(CategoriesPaginationDto categoriesPagination
            , CancellationToken cancellationToken);
    }
}
