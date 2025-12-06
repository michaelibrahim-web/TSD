using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Entities;
using TSD.Contract.Request;
using TSD.Contract.Response;

namespace TSD.Domain.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<CategoryResponse> GetCategoryByIdAsync(int id);
        Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync();

        // Operation to create a new category
        Task<CategoryResponse> CreateCategoryAsync(CreateCategoryRequest newCategory);

        // Operation to modify an existing category
        Task UpdateCategoryAsync(CreateCategoryRequest updatedCategory);

        // Operation to delete a category
        Task DeleteCategoryAsync(int id);
    }
}
