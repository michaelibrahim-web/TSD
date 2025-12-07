using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Entities;
using TSD.Contract.Response;
using TSD.Contract.Request;

namespace TSD.Domain.Interfaces.Services
{
    public interface IProjectService
    {
        Task<ProjectResponse> GetProjectByIdAsync(int id);
        Task<IEnumerable<ProjectResponse>> GetAllProjectsAsync();
        Task<IEnumerable<ProjectResponse>> GetProjectsByClientAsync(int clientId);
        Task<ProjectResponse> CreateProjectAsync(CreateProjectRequest newProject);
        Task<ProjectResponse> UpdateAsync(int id, UpdateProjectRequest request);
        Task<bool> DeleteAsync(int id);


    }
}
