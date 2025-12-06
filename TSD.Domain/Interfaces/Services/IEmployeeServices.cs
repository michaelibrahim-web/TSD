using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Contract.Response;
using TSD.Domain.Entities;
using TSD.Contract.Enums;
using TSD.Contract.Request;

namespace TSD.Domain.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeResponse> GetEmployeeByIdAsync(int id);
        Task<IEnumerable<EmployeeResponse>> GetAllEmployeesAsync();
        Task<IEnumerable<EmployeeResponse>> GetEmployeesByRoleAsync(EmployeeRole role);
        Task<EmployeeResponse> CreateEmployeeAsync(CreateEmployeeRequest newEmployee);
        Task UpdateEmployeeAsync(UpdateEmployeeRequest updatedEmployee);
        Task DeleteEmployeeAsync(int id);
        Task ChangeEmployeeStatusAsync(int id, EmployeeStatus newStatus);
        Task<EmployeeResponse> LoginAsync(EmployeeLoginRequest request);
    }
}
