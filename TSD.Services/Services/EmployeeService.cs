using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Contract.Request;
using TSD.Contract.Response;
using TSD.Domain.Entities;
using TSD.Contract.Enums;
using TSD.Domain.Exceptions;
using TSD.Domain.Interfaces.Repository;
using TSD.Domain.Interfaces.Services;
using TSD.Services.Mapping;

namespace TSD.Services.Services
{
    
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, IPasswordService passwordService)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        public async Task<EmployeeResponse> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            // Business Rule: Throw a domain exception if not found (for API layer translation)
            if (employee == null)
            {
                throw new EntityNotFoundException(nameof(Employee), id);
            }

            return _mapper.Map<EmployeeResponse>(employee);
        }

        public async Task<IEnumerable<EmployeeResponse>> GetAllEmployeesAsync()
        {
            var results = await _employeeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EmployeeResponse>>(results);
        }

        public async Task<IEnumerable<EmployeeResponse>> GetEmployeesByRoleAsync(EmployeeRole role)
        {
            var employeeRole=await _employeeRepository.GetEmployeesByRoleAsync(role);
            return _mapper.Map<IEnumerable<EmployeeResponse>>(employeeRole);
        }

        public async Task<EmployeeResponse> CreateEmployeeAsync(CreateEmployeeRequest request)
        {
            // Business Rule: Ensure username is unique before saving
            var existingEmployee = await _employeeRepository.GetByUserNameAsync(request.UserName);
            if (existingEmployee != null)
            {
                throw new InvalidOperationException($"Username '{request.UserName}' is already taken.");
            }

            // NOTE: Password hashing logic (e.g., using BCrypt) would be implemented here before saving.
            // For now, we set the initial status
      
            var newEmployee = _mapper.Map<Employee>(request);
            var hashedPassword = _passwordService.HashPassword(newEmployee.Password);
            newEmployee.Password = hashedPassword;
            newEmployee.Status = EmployeeStatus.Active; // Default status
            

            await _employeeRepository.AddAsync(newEmployee);
            await _employeeRepository.SaveChangesAsync(); // Final database commit

            return _mapper.Map<EmployeeResponse>(newEmployee);
        }

        public async Task UpdateEmployeeAsync(UpdateEmployeeRequest updatedEmployee)
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
            existing.Role =updatedEmployee.Role;
            existing.HoursPerWeek = updatedEmployee.HoursPerWeek;

            _employeeRepository.Update(_mapper.Map<Employee>(existing));
            await _employeeRepository.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            // Load the entity directly from EF (tracked instance)
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                throw new Exception("Employee not found");

            // Add any domain rules here (if needed)
            // Example:
            // if (employee.HasUnapprovedTimeEntries)
            //     throw new Exception("Cannot delete employee with unapproved entries.");

            _employeeRepository.Delete(employee);  // ✔ delete tracked entity
            await _employeeRepository.SaveChangesAsync();
        }


        public async Task ChangeEmployeeStatusAsync(int id, EmployeeStatus newStatus)
        {
            var employee = await _employeeRepository.GetByIdAsync(id); // tracked entity

            if (employee == null)
                throw new EntityNotFoundException($"Employee with Id {id} was not found.");

           

            // Update directly
            employee.Status = newStatus;

            _employeeRepository.Update(employee);   // tracked entity
            await _employeeRepository.SaveChangesAsync();
        }
        //login 
        public async Task<EmployeeResponse> LoginAsync(EmployeeLoginRequest request)
        {
            var employee = await _employeeRepository.GetByUserNameAsync(request.username);
            if (employee == null) throw new Exception("Invalid username or password.");

            // Verify password
            bool isValid = _passwordService.VerifyPassword(request.password, employee.Password);
            if (!isValid) throw new Exception("Invalid username or password.");

            return _mapper.Map<EmployeeResponse>(employee);
        }


    }
}
