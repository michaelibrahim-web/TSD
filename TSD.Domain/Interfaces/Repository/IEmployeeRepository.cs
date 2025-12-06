using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Entities;
using TSD.Contract.Request;
using TSD.Contract.Response;
using TSD.Contract.Enums;

namespace TSD.Domain.Interfaces.Repository
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
       
        Task<Employee?> GetByUserNameAsync(string userName);
        Task<IEnumerable<Employee>> GetEmployeesByRoleAsync(EmployeeRole role);
        Task<IEnumerable<Employee>> GetActiveEmployeesAsync();
        Task AddAsync(Employee newEmployee);
        void Update(Employee existing);
        void Delete(Employee employeeToDelete);
    }
}
