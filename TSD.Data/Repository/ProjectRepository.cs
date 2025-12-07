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
        private readonly TSD_DbContext _context;
        public ProjectRepository(TSD_DbContext context) : base(context)
        {
            _context = context;
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

        public async Task<Project> AddProjectAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return project; // now project.Id is populated
        }
        public async Task<Project> UpdateAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
