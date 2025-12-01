using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Entities;

namespace TSD.Domain.Interfaces.Repository
{
    public interface ITeamMemberRepository : IGenericRepository<TeamMember>
    {
       
        Task<IEnumerable<TeamMember>> GetTeamMembersByProjectIdAsync(int projectId);

       
        Task<TeamMember?> GetTeamMemberByEmployeeAndProjectAsync(int employeeId, int projectId);
    }
}
