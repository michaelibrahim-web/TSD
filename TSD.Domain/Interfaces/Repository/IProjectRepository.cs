using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Entities;

namespace TSD.Domain.Interfaces.Repository
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
       
        Task<IEnumerable<Project>> GetProjectsByClientAsync(int clientId);
        Task<IEnumerable<Project>> GetProjectsByLeadAsync(int leadId);
        Task<bool> IsProjectNameUniqueAsync(string projectName, int? projectId = null);
        Task<Project> AddProjectAsync(Project project);
        Task<Project> UpdateAsync(Project project);
        Task<bool> DeleteAsync(int id);
    }
}
