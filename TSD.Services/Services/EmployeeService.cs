using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Entities;
using TSD.Domain.Enums;
using TSD.Domain.Exceptions;
using TSD.Domain.Interfaces.Repository;
using TSD.Domain.Interfaces.Services;

namespace TSD.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            // Business Rule: Throw a domain exception if not found (for API layer translation)
            if (employee == null)
            {
                throw new EntityNotFoundException(nameof(Employee), id);
            }

            return employee;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByRoleAsync(EmployeeRole role)
        {
            return await _employeeRepository.GetEmployeesByRoleAsync(role);
        }

        public async Task<Employee> CreateEmployeeAsync(Employee newEmployee)
        {
            // Business Rule: Ensure username is unique before saving
            var existingEmployee = await _employeeRepository.GetByUserNameAsync(newEmployee.UserName);
            if (existingEmployee != null)
            {
                throw new InvalidOperationException($"Username '{newEmployee.UserName}' is already taken.");
            }

            // NOTE: Password hashing logic (e.g., using BCrypt) would be implemented here before saving.
            // For now, we set the initial status
            newEmployee.Status = EmployeeStatus.Active;

            await _employeeRepository.AddAsync(newEmployee);
            await _employeeRepository.SaveChangesAsync(); // Final database commit

            return newEmployee;
        }

        public async Task UpdateEmployeeAsync(Employee updatedEmployee)
        {
            var existing = await GetEmployeeByIdAsync(updatedEmployee.Id); // Uses the validation logic

            // Business Rule: Check for username uniqueness if the username changed
            if (existing.UserName != updatedEmployee.UserName)
            {
                var existingByUsername = await _employeeRepository.GetByUserNameAsync(updatedEmployee.UserName);
                if (existingByUsername != null)
                {
                    throw new InvalidOperationException($"Username '{updatedEmployee.UserName}' is already taken by another employee.");
                }
                existing.UserName = updatedEmployee.UserName;
            }

            // Update mutable properties
            existing.FullName = updatedEmployee.FullName;
            existing.Email = updatedEmployee.Email;
            existing.Role = updatedEmployee.Role;
            existing.HoursPerWeek = updatedEmployee.HoursPerWeek;

            _employeeRepository.Update(existing);
            await _employeeRepository.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employeeToDelete = await GetEmployeeByIdAsync(id);

            // NOTE: Add logic here to check if the employee has any unarchived projects led 
            // or unapproved time entries before deleting.

            _employeeRepository.Delete(employeeToDelete);
            await _employeeRepository.SaveChangesAsync();
        }

        public async Task ChangeEmployeeStatusAsync(int id, EmployeeStatus newStatus)
        {
            var employee = await GetEmployeeByIdAsync(id);

            // Business Rule: Cannot set status to active if the hours per week is zero
            if (newStatus == EmployeeStatus.Active && employee.HoursPerWeek == 0)
            {
                throw new InvalidOperationException("Cannot set employee to Active status when HoursPerWeek is 0.");
            }

            employee.Status = newStatus;

            _employeeRepository.Update(employee);
            await _employeeRepository.SaveChangesAsync();
        }
    }
}
