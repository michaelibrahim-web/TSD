using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Entities;
using TSD.Domain.Enums;

namespace TSD.Domain.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<IEnumerable<Employee>> GetEmployeesByRoleAsync(EmployeeRole role);
        Task<Employee> CreateEmployeeAsync(Employee newEmployee);
        Task UpdateEmployeeAsync(Employee updatedEmployee);
        Task DeleteEmployeeAsync(int id);
        Task ChangeEmployeeStatusAsync(int id, EmployeeStatus newStatus);
    }
}
