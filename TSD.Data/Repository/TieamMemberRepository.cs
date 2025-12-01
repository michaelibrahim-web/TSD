using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Entities;
using TSD.Domain.Interfaces.Repository;

namespace TSD.Data.Repository
{
    public class TeamMemberRepository : GenericRepository<TeamMember>, ITeamMemberRepository
    {
        public TeamMemberRepository(TSD_DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TeamMember>> GetTeamMembersByProjectIdAsync(int projectId)
        {
            return await _dbSet
                .Where(tm => tm.ProjectId == projectId)
                .Include(tm => tm.Employee) // Eager load employee details
                .ToListAsync();
        }

        public async Task<TeamMember?> GetTeamMemberByEmployeeAndProjectAsync(int employeeId, int projectId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(tm => tm.EmployeeId == employeeId && tm.ProjectId == projectId);
        }
    }
}
