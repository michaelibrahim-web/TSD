using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Entities;

namespace TSD.Domain.Interfaces.Services
{
    public interface IProjectService
    {
        Task<Project> GetProjectByIdAsync(int id);
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<IEnumerable<Project>> GetProjectsByClientAsync(int clientId);
        Task<Project> CreateProjectAsync(Project newProject);
        Task UpdateProjectAsync(Project updatedProject);
        Task ArchiveProjectAsync(int id);
    }
}
