using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Entities;
using TSD.Domain.Exceptions;
using TSD.Domain.Interfaces.Repository;
using TSD.Domain.Interfaces.Services;

namespace TSD.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new EntityNotFoundException(nameof(Category), id);
            }
            return category;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> CreateCategoryAsync(Category newCategory)
        {
            // --- Business Rule: Ensure Category Name is Unique ---
            var existingCategory = await _categoryRepository.GetByNameAsync(newCategory.CategoryName);
            if (existingCategory != null)
            {
                throw new InvalidOperationException($"Category name '{newCategory.CategoryName}' already exists.");
            }

            await _categoryRepository.AddAsync(newCategory);
            await _categoryRepository.SaveChangesAsync();
            return newCategory;
        }

        public async Task UpdateCategoryAsync(Category updatedCategory)
        {
            var existingCategory = await GetCategoryByIdAsync(updatedCategory.Id); // Existence check

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
            var categoryToDelete = await GetCategoryByIdAsync(id);

            // NOTE: In a robust application, you would check ITimeEntryRepository here 
            // to ensure no TimeEntries reference this category before deletion.

            _categoryRepository.Delete(categoryToDelete);
            await _categoryRepository.SaveChangesAsync();
        }
    }
}
