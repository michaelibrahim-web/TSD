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
    public class TeamMemberService : ITeamMemberService
    {
        private readonly ITeamMemberRepository _teamMemberRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public TeamMemberService(
            ITeamMemberRepository teamMemberRepository,
            IProjectRepository projectRepository,
            IEmployeeRepository employeeRepository)
        {
            _teamMemberRepository = teamMemberRepository;
            _projectRepository = projectRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<TeamMember> GetTeamMemberByIdAsync(int id)
        {
            var member = await _teamMemberRepository.GetByIdAsync(id);
            if (member == null)
            {
                throw new EntityNotFoundException(nameof(TeamMember), id);
            }
            return member;
        }

        public async Task<IEnumerable<TeamMember>> GetTeamMembersByProjectAsync(int projectId)
        {
            // Business Rule: Ensure the project exists
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null)
            {
                throw new EntityNotFoundException(nameof(Project), projectId);
            }

            return await _teamMemberRepository.GetTeamMembersByProjectIdAsync(projectId);
        }

        public async Task<TeamMember> AddTeamMemberAsync(int projectId, int employeeId)
        {
            // --- Business Rule 1: Validate dependencies ---
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null) throw new EntityNotFoundException(nameof(Project), projectId);

            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null) throw new EntityNotFoundException(nameof(Employee), employeeId);

            // --- Business Rule 2: Check for Existing Membership ---
            var existingMember = await _teamMemberRepository.GetTeamMemberByEmployeeAndProjectAsync(employeeId, projectId);
            if (existingMember != null)
            {
                throw new InvalidOperationException($"Employee {employeeId} is already a member of project {projectId}.");
            }

            // Create the new join entity
            var newTeamMember = new TeamMember
            {
                EmployeeId = employeeId,
                ProjectId = projectId,
                FullName = employee.FullName, // Denormalizing the name for quick access
                Role = employee.Role,         // Taking the employee's default role
                IsActive = true
            };

            await _teamMemberRepository.AddAsync(newTeamMember);
            await _teamMemberRepository.SaveChangesAsync();
            return newTeamMember;
        }

        public async Task UpdateTeamMemberAsync(TeamMember updatedTeamMember)
        {
            var existingMember = await GetTeamMemberByIdAsync(updatedTeamMember.Id); // Existence check

            // Update only the allowed fields on the join table
            existingMember.Role = updatedTeamMember.Role;
            existingMember.Department = updatedTeamMember.Department;
            existingMember.IsActive = updatedTeamMember.IsActive;

            _teamMemberRepository.Update(existingMember);
            await _teamMemberRepository.SaveChangesAsync();
        }

        public async Task RemoveTeamMemberAsync(int teamMemberId)
        {
            var memberToDelete = await GetTeamMemberByIdAsync(teamMemberId);

            // Business Rule: Cannot remove the project lead via this method (ProjectService handles lead changes)
            var project = await _projectRepository.GetByIdAsync(memberToDelete.ProjectId);
            if (project != null && project.LeadId == memberToDelete.EmployeeId)
            {
                throw new InvalidOperationException("Cannot remove the Project Lead from the team. Change the lead first.");
            }

            _teamMemberRepository.Delete(memberToDelete);
            await _teamMemberRepository.SaveChangesAsync();
        }

        Task ITeamMemberService.AddTeamMemberAsync(int projectId, int employeeId)
        {
            return AddTeamMemberAsync(projectId, employeeId);
        }
    }
}
