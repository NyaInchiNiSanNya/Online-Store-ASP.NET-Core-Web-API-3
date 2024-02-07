using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        public async Task<CategoryDto?> GetCategoryByIdAsync(Int32 categoryId)
        {
            if (categoryId < 0)
            {
                throw new ArgumentException(nameof(categoryId));
            }

            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<Boolean> DeleteCategoryByIdAsync(Int32 categoryId)
        {
            if (categoryId < 0)
            {
                throw new ArgumentException(nameof(categoryId));
            }

            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);

            if (category == null)
            {
                return false;
            }

            await _unitOfWork.Categories.Remove(categoryId);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<Boolean> CreateNewCategoryAsync(CategoryDto newCategory)
        {
            var category= await _unitOfWork.Categories
                .FindBy(x => x.Name.Equals(newCategory.Name))
                .FirstOrDefaultAsync();

            if (category != null)
            {
                return false;
            }

            var newCategoryToCreate = _mapper.Map<Сategory>(newCategory);

            await _unitOfWork.Categories.AddAsync(newCategoryToCreate);

            await _unitOfWork.SaveChangesAsync();
            
            return true;
        }

        public async Task<Boolean> UpdateCategoryAsync(CategoryDto newCategory)
        {
            if (newCategory.Id < 1)
            {
                return false;
            }

            var category = await _unitOfWork.Categories.GetByIdAsync(newCategory.Id);

            if (category == null)
            {
                return false;
            }

            var patchDtos = new List<Patch> {

                    new Patch { PropertyName = "Name", PropertyValue = newCategory.Name },
                };

            await _unitOfWork.Categories.PatchAsync(newCategory.Id, patchDtos);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
