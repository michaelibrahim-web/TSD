using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Entities;

namespace TSD.Domain.Interfaces.Services
{
    public interface ITeamMemberService
    {
        Task<TeamMember> GetTeamMemberByIdAsync(int id);
        Task<IEnumerable<TeamMember>> GetTeamMembersByProjectAsync(int projectId);
        Task AddTeamMemberAsync(int projectId, int employeeId);
        Task RemoveTeamMemberAsync(int teamMemberId);
    }
}
