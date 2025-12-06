using Microsoft.AspNetCore.Mvc;
using TSD.Contract.Request;
using TSD.Contract.Response;
using TSD.Domain.Exceptions;
using TSD.Domain.Interfaces.Services;

namespace TSD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamMemberController : ControllerBase
    {
        private readonly ITeamMemberService _teamMemberService;

        public TeamMemberController(ITeamMemberService teamMemberService)
        {
            _teamMemberService = teamMemberService;
        }

        // GET: api/TeamMember/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeamMemberById(int id)
        {
            try
            {
                var member = await _teamMemberService.GetTeamMemberByIdAsync(id);
                return Ok(member);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/TeamMember/project/{projectId}
        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetTeamMembersByProject(int projectId)
        {
            try
            {
                var members = await _teamMemberService.GetTeamMembersByProjectAsync(projectId);
                return Ok(members);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/TeamMember
        [HttpPost]
        public async Task<IActionResult> AddTeamMember([FromBody] AddTeamMemberRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newMember = await _teamMemberService.AddTeamMemberAsync(request);
                return CreatedAtAction(nameof(GetTeamMemberById), new { id = newMember.Id }, newMember);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/TeamMember/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeamMember(int id, [FromBody] UpdateTeamMemberRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != request.Id)
                return BadRequest("TeamMember ID mismatch.");

            try
            {
                await _teamMemberService.UpdateTeamMemberAsync(request);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/TeamMember/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveTeamMember(int id)
        {
            try
            {
                await _teamMemberService.RemoveTeamMemberAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
