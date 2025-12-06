using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Entities;
using TSD.Domain.Exceptions;
using TSD.Domain.Interfaces.Repository;
using TSD.Domain.Interfaces.Services;
using TSD.Contract.Response;
using TSD.Contract.Request;

namespace TSD.Services.Services
{
    public class TeamMemberService : ITeamMemberService
    {
        private readonly ITeamMemberRepository _teamMemberRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public TeamMemberService(
            ITeamMemberRepository teamMemberRepository,
            IProjectRepository projectRepository,
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _teamMemberRepository = teamMemberRepository;
            _projectRepository = projectRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<TeamMemberResponse> GetTeamMemberByIdAsync(int id)
        {
            var member = await _teamMemberRepository.GetByIdAsync(id);
            if (member == null)
            {
                throw new EntityNotFoundException(nameof(TeamMember), id);
            }
            return _mapper.Map<TeamMemberResponse>(member);
        }

        public async Task<IEnumerable<TeamMemberResponse>> GetTeamMembersByProjectAsync(int projectId)
        {
            // Business Rule: Ensure the project exists
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null)
            {
                throw new EntityNotFoundException(nameof(Project), projectId);
            }

            return _mapper.Map<IEnumerable<TeamMemberResponse>>(await _teamMemberRepository.GetTeamMembersByProjectIdAsync(projectId));
        }

        public async Task<TeamMemberResponse> AddTeamMemberAsync(AddTeamMemberRequest member)
        {
            // --- Business Rule 1: Validate dependencies ---
            var project = await _projectRepository.GetByIdAsync(member.ProjectId);
            if (project == null) throw new EntityNotFoundException(nameof(Project), member.ProjectId);

            var employee = await _employeeRepository.GetByIdAsync(member.EmployeeId);
            if (employee == null) throw new EntityNotFoundException(nameof(Employee), member.EmployeeId);

            // --- Business Rule 2: Check for Existing Membership ---
            var existingMember = await _teamMemberRepository.GetTeamMemberByEmployeeAndProjectAsync(member.EmployeeId, member.ProjectId);
            if (existingMember != null)
            {
                throw new InvalidOperationException($"Employee {member.EmployeeId} is already a member of project {member.ProjectId}.");
            }

            // Create the new join entity
            var newTeamMember = new TeamMember
            {
                EmployeeId = member.EmployeeId,
                ProjectId =member.ProjectId,
                FullName = employee.FullName, // Denormalizing the name for quick access
                Role = employee.Role,         // Taking the employee's default role
                IsActive = true
            };

            await _teamMemberRepository.AddAsync(newTeamMember);
            await _teamMemberRepository.SaveChangesAsync();
            return _mapper.Map<TeamMemberResponse>(newTeamMember);
        }

        public async Task UpdateTeamMemberAsync(UpdateTeamMemberRequest updatedTeamMember)
        {
            var existingMember = await GetTeamMemberByIdAsync(updatedTeamMember.Id); // Existence check

            // Update only the allowed fields on the join table
            existingMember.Role = updatedTeamMember.Role;
            existingMember.Department = updatedTeamMember.Department;
            existingMember.IsActive = updatedTeamMember.IsActive;

            _teamMemberRepository.Update(_mapper.Map<TeamMember>(existingMember));
            await _teamMemberRepository.SaveChangesAsync();
        }

        public async Task RemoveTeamMemberAsync(int teamMemberId)
        {
            var memberToDelete = await _teamMemberRepository.GetByIdAsync(teamMemberId);

            // Business Rule: Cannot remove the project lead via this method (ProjectService handles lead changes)
            var project = await _projectRepository.GetByIdAsync(memberToDelete.ProjectId);
            if (project != null && project.LeadId == memberToDelete.EmployeeId)
            {
                throw new InvalidOperationException("Cannot remove the Project Lead from the team. Change the lead first.");
            }

            _teamMemberRepository.Delete(memberToDelete);
            await _teamMemberRepository.SaveChangesAsync();
        }

        
    }
}
