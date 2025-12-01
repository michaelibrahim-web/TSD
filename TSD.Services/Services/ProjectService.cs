using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Entities;
using TSD.Domain.Exceptions;
using TSD.Domain.Interfaces.Repository;
using TSD.Domain.Interfaces.Services;

namespace TSD.Services.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);

            if (project == null)
                throw new EntityNotFoundException($"Project with ID {id} was not found.");

            return project;
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _projectRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Project>> GetProjectsByClientAsync(int clientId)
        {
            return await _projectRepository.GetProjectsByClientAsync(clientId);
        }

        public async Task<Project> CreateProjectAsync(Project newProject)
        {
            // Business rule example: project name must be unique
            bool isUnique = await _projectRepository.IsProjectNameUniqueAsync(newProject.ProjectName);

            if (!isUnique)
                throw new EntityNotFoundException($"A project with the name '{newProject.ProjectName}' already exists.");

            await _projectRepository.AddAsync(newProject);
            return newProject;
        }

        public async Task UpdateProjectAsync(Project updatedProject)
        {
            var existing = await _projectRepository.GetByIdAsync(updatedProject.Id);

            if (existing == null)
                throw new EntityNotFoundException($"Cannot update. Project with ID {updatedProject.Id} not found.");

            // Optional business check: uniqueness but allow updating itself
            bool isUnique = await _projectRepository.IsProjectNameUniqueAsync(
                updatedProject.ProjectName,
                updatedProject.Id
            );

            if (!isUnique)
                throw new EntityNotFoundException($"Another project with the name '{updatedProject.ProjectName}' already exists.");

            await _projectRepository.UpdateProjectAsync(updatedProject);
        }

        public async Task ArchiveProjectAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);

            if (project == null)
                throw new EntityNotFoundException($"Cannot archive. Project with ID {id} not found.");

            project.Archive = true;    // Assuming your Project entity has IsArchived property

            await _projectRepository.UpdateProjectAsync(project);
        }
    }
}
