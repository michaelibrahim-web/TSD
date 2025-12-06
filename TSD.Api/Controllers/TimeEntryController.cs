using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TSD.Contract.Request;
using TSD.Contract.Response;
using TSD.Domain.Interfaces.Services;

namespace TSD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeEntryController : ControllerBase
    {
        private readonly ITimeEntryService _timeEntryService;

        public TimeEntryController(ITimeEntryService timeEntryService)
        {
            _timeEntryService = timeEntryService;
        }

        // GET: api/TimeEntry/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TimeEntryResponse>> GetTimeEntryById(int id)
        {
            var result = await _timeEntryService.GetTimeEntryByIdAsync(id);
            return Ok(result);
        }

      
       
        // POST: api/TimeEntry
        [HttpPost]
        public async Task<ActionResult<TimeEntryResponse>> LogTime([FromBody] CreatTimeEntryRequest request)
        {
            var result = await _timeEntryService.LogTimeAsync(request);
            return CreatedAtAction(nameof(GetTimeEntryById), new { id = result.Id }, result);
        }

        // PUT: api/TimeEntry/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimeEntry(int id, [FromBody] CreatTimeEntryRequest request)
        {
            if (id != request.Id)
                return BadRequest("ID mismatch.");

            await _timeEntryService.UpdateTimeEntryAsync(request);
            return NoContent();
        }

        // DELETE: api/TimeEntry/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeEntry(int id)
        {
            await _timeEntryService.DeleteTimeEntryAsync(id);
            return NoContent();
        }
    }
}
