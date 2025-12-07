using Microsoft.AspNetCore.Mvc;
using TSD.Contract.Request;
using TSD.Contract.Response;
using TSD.Domain.Entities;
using TSD.Domain.Exceptions;
using TSD.Domain.Interfaces.Services;

namespace TSD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // GET: api/Project/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            try
            {
                var project = await _projectService.GetProjectByIdAsync(id);
                return Ok(project);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/Project
        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }

        // GET: api/Project/client/{clientId}
        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetProjectsByClient(int clientId)
        {
            var projects = await _projectService.GetProjectsByClientAsync(clientId);
            return Ok(projects);
        }

        // POST: api/Project
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdProject = await _projectService.CreateProjectAsync(request);
                return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.Id }, createdProject);
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProjectRequest request)
        {
            var updated = await _projectService.UpdateAsync(id, request);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _projectService.DeleteAsync(id);
            if (!deleted) return NotFound("Project not found");
            return Ok("Project deleted successfully");
        }


    }
}
