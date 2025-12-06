using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Entities;
using TSD.Contract.Enums;
using TSD.Domain.Interfaces.Repository;

namespace TSD.Data.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(TSD_DbContext context) : base(context)
        {
        }

        // Implementation of specific query from IEmployeeRepository
        public async Task<Employee?> GetByUserNameAsync(string userName)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.UserName == userName);
        }

        // Implementation of specific query from IEmployeeRepository
        public async Task<IEnumerable<Employee>> GetEmployeesByRoleAsync(EmployeeRole role)
        {
            return await _dbSet.Where(e => e.Role == role).ToListAsync();
        }

        // Implementation of specific query from IEmployeeRepository
        public async Task<IEnumerable<Employee>> GetActiveEmployeesAsync()
        {
            return await _dbSet.Where(e => e.Status == EmployeeStatus.Active).ToListAsync();
        }
    }
}
