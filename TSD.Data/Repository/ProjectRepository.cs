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
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(TSD_DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Project>> GetProjectsByClientAsync(int clientId)
        {
            return await _dbSet
                .Where(p => p.ClientId == clientId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetProjectsByLeadAsync(int leadId)
        {
            return await _dbSet
                .Where(p => p.LeadId == leadId)
                .ToListAsync();
        }

        public async Task<bool> IsProjectNameUniqueAsync(string projectName, int? projectId = null)
        {
            return !await _dbSet.AnyAsync(p =>
                p.ProjectName == projectName &&
                (!projectId.HasValue || p.Id != projectId.Value));
        }

        public Task UpdateProjectAsync(Project project)
        {
            throw new NotImplementedException();
        }
    }
}
