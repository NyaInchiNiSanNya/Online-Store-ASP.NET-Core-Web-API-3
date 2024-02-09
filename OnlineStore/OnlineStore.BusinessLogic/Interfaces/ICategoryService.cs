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
        public Task<CategoryDto?> GetCategoryByIdAsync(Int32 categoryId);

        public Task<Boolean> DeleteCategoryByIdAsync(Int32 categoryId);

        public Task<Int32> CreateNewCategoryAsync(CategoryDto newCategory);

        public Task<Boolean> UpdateCategoryAsync(CategoryDto newCategory);

        public Task<IEnumerable<CategoryDto>?> GetCategoriesByPageAsync(Int32 page, Int32 pageSize);
    }
}
