using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Entities;

namespace TSD.Domain.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<Category> GetCategoryByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();

        // Operation to create a new category
        Task<Category> CreateCategoryAsync(Category newCategory);

        // Operation to modify an existing category
        Task UpdateCategoryAsync(Category updatedCategory);

        // Operation to delete a category
        Task DeleteCategoryAsync(int id);
    }
}
