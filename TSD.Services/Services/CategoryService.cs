using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Contract.Request;
using TSD.Contract.Response;
using TSD.Domain.Entities;
using TSD.Domain.Exceptions;
using TSD.Domain.Interfaces.Repository;
using TSD.Domain.Interfaces.Services;
namespace TSD.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryResponse> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new EntityNotFoundException(nameof(Category), id);
            }
            return _mapper.Map<CategoryResponse>(category);
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync()
        {
            return _mapper.Map< IEnumerable < CategoryResponse >> (await _categoryRepository.GetAllAsync());
        }

        public async Task<CategoryResponse> CreateCategoryAsync(CreateCategoryRequest result)
        {
            // --- Business Rule: Ensure Category Name is Unique ---
            var existingCategory = await _categoryRepository.GetByNameAsync(result.CategoryName);
            if (existingCategory != null)
            {
                throw new InvalidOperationException($"Category name '{result.CategoryName}' already exists.");
            }

            var categoryEntity = _mapper.Map<Category>(result);
            await _categoryRepository.AddAsync(categoryEntity);
            await _categoryRepository.SaveChangesAsync();
            return _mapper.Map<CategoryResponse>(categoryEntity) ;
        }

        public async Task UpdateCategoryAsync(CreateCategoryRequest updatedCategory)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(updatedCategory.Id); // Existence check

            // --- Business Rule: Check for unique name upon update ---
            if (existingCategory.CategoryName != updatedCategory.CategoryName)
            {
                var nameConflict = await _categoryRepository.GetByNameAsync(updatedCategory.CategoryName);
                if (nameConflict != null && nameConflict.Id != updatedCategory.Id)
                {
                    throw new InvalidOperationException($"Category name '{updatedCategory.CategoryName}' is already in use.");
                }
            }

            existingCategory.CategoryName = updatedCategory.CategoryName;

            _categoryRepository.Update(existingCategory);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            // ✔ Fetch the actual tracked entity from the database
            var existingCategory = await _categoryRepository.GetByIdAsync(id);

            if (existingCategory == null)
                throw new EntityNotFoundException(nameof(Category), id);

            // NOTE: Add validation checks here (e.g., existing time entries)

            // ✔ Delete the tracked entity (no mapping needed)
            _categoryRepository.Delete(existingCategory);

            await _categoryRepository.SaveChangesAsync();
        }

    }
}
