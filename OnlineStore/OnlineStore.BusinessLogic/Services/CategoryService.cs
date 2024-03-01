using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.BusinessLogic.Excpetions;
using OnlineStore.BusinessLogic.Interfaces;
using OnlineStore.Data.Entities;
using OnlineStore.Data.Interfaces;
using OnlineStore.DTO.DTO;
using OnlineStore.DTO.Models;

namespace OnlineStore.BusinessLogic.Services
{
    internal class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int categoryId, CancellationToken cancellationToken)
        {
            if (categoryId < 1)
            {
                throw new InvalidIdException($"Id {categoryId} is invalid");
            }

            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId, cancellationToken);

            if (category == null)
            {
                throw new ObjectNotFoundException($"Category {categoryId} not found");
            }

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task DeleteCategoryByIdAsync(int categoryId, CancellationToken cancellationToken)
        {
            if (categoryId < 1)
            {
                throw new InvalidIdException($"Id {categoryId} is invalid");
            }

            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId, cancellationToken);

            if (category == null)
            {
                throw new ObjectNotFoundException($"Category {categoryId} not found");
            }

            await _unitOfWork.Categories.RemoveAsync(categoryId, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

        }

        public async Task CreateNewCategoryAsync(CategoryDto newCategory, CancellationToken cancellationToken)
        {
            var category= await _unitOfWork.Categories
                .FindBy(x => x.Name.Equals(newCategory.Name))
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (category != null)
            {
                throw new ObjectAlreadyExistException($"Category {newCategory.Name} already exist");
            }

            var newCategoryToCreate = _mapper.Map<Сategory>(newCategory);

            await _unitOfWork.Categories.AddAsync(newCategoryToCreate, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

        }

        public async Task UpdateCategoryAsync(CategoryDto newCategory, CancellationToken cancellationToken)
        {
            if (newCategory.Id < 1)
            {
                throw new InvalidIdException($"Id {newCategory.Id} is invalid");
            }

            var category = await _unitOfWork.Categories.GetByIdAsync(newCategory.Id, cancellationToken);

            if (category == null)
            {
                throw new ObjectNotFoundException($"Category {newCategory.Id} not found");
            }

            var patchDto = new List<Patch> {

                    new Patch { PropertyName = "Name", PropertyValue = newCategory.Name },
                };

            await _unitOfWork.Categories.PatchAsync(newCategory.Id, patchDto, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<CategoriesDto?> GetCategoriesByPageAsync(PaginationDto paginationDto
            , CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.Categories
                .GetCategoriesByPageAsync(paginationDto.Page, paginationDto.PageSize, cancellationToken); 

            var categoriesDtoList = _mapper.Map<List<CategoryDto>>(categories);

            var totalCategoriesCount = await _unitOfWork.Categories.CountAsync(cancellationToken);

            var categoriesDto = new CategoriesDto()
            {
                Categories = categoriesDtoList,
                TotalCategoriesCount = totalCategoriesCount
            };

            return categoriesDto;
        }
    }
}
