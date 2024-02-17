using OnlineStore.DTO.DTO;

namespace OnlineStore.BusinessLogic.Interfaces
{
    public interface ICategoryService
    {
        public Task<CategoryDto?> GetCategoryByIdAsync(int categoryId, CancellationToken cancellationToken);

        public Task DeleteCategoryByIdAsync(int categoryId, CancellationToken cancellationToken);

        public Task CreateNewCategoryAsync(CategoryDto newCategory, CancellationToken cancellationToken);

        public Task UpdateCategoryAsync(CategoryDto newCategory, CancellationToken cancellationToken);

        public Task<IEnumerable<CategoryDto>?> GetCategoriesByPageAsync(CategoriesPaginationDto categoriesPagination
            , CancellationToken cancellationToken);
    }
}
