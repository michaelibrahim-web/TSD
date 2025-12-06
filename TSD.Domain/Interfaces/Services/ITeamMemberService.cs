using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Entities;
using TSD.Contract.Request;
using TSD.Contract.Response;
namespace TSD.Domain.Interfaces.Services
{
    public interface ITeamMemberService
    {
        Task<TeamMemberResponse> GetTeamMemberByIdAsync(int id);
        Task<IEnumerable<TeamMemberResponse>> GetTeamMembersByProjectAsync(int projectId);
       Task<TeamMemberResponse> AddTeamMemberAsync(AddTeamMemberRequest member);
        Task RemoveTeamMemberAsync(int teamMemberId);
        Task UpdateTeamMemberAsync(UpdateTeamMemberRequest updatedTeamMember);
    }
}
