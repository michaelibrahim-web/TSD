using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Contract.Request;
using TSD.Contract.Response;
using TSD.Domain.Entities;
using TSD.Domain.Exceptions;
using TSD.Domain.Interfaces.Repository;
using TSD.Domain.Interfaces.Services;

namespace TSD.Services.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IMapper mapping)
        {
            _projectRepository = projectRepository;
            _mapper = mapping;
        }

        public async Task<ProjectResponse> GetProjectByIdAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);

            if (project == null)
                throw new EntityNotFoundException($"Project with ID {id} was not found.");

            return _mapper.Map<ProjectResponse>(project);
        }

        public async Task<IEnumerable<ProjectResponse>> GetAllProjectsAsync()
        {
            return _mapper.Map<IEnumerable<ProjectResponse>>(await _projectRepository.GetAllAsync());
        }

        public async Task<IEnumerable<ProjectResponse>> GetProjectsByClientAsync(int clientId)
        {
            return _mapper.Map<IEnumerable<ProjectResponse>>(await _projectRepository.GetAllAsync());
        }

        public async Task<ProjectResponse> CreateProjectAsync(CreateProjectRequest newProject)
        {
            // Business rule example: project name must be unique
            bool isUnique = await _projectRepository.IsProjectNameUniqueAsync(newProject.ProjectName);

            if (!isUnique)
                throw new EntityNotFoundException($"A project with the name '{newProject.ProjectName}' already exists.");

           var result= await _projectRepository.AddProjectAsync(_mapper.Map<Project>(newProject));

            return _mapper.Map<ProjectResponse>(result);
        }
        public async Task<ProjectResponse> UpdateAsync(int id, UpdateProjectRequest request)
        {
            var existing = await _projectRepository.GetByIdAsync(id);
            if (existing == null) throw new Exception("Project not found");

            _mapper.Map(request, existing);

            await _projectRepository.UpdateAsync(existing);
            return _mapper.Map<ProjectResponse>(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _projectRepository.DeleteAsync(id);
        }




    }
}
