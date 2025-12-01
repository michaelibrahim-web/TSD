using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Entities;

namespace TSD.Domain.Interfaces.Repository
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        
        Task<Category?> GetByNameAsync(string name);
    }
}
