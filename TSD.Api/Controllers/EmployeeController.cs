using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TSD.Contract.Enums;
using TSD.Contract.Request;
using TSD.Contract.Response;
using TSD.Domain.Interfaces.Services;

namespace TSD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // POST: api/employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employeeResponse = await _employeeService.CreateEmployeeAsync(request);

            return CreatedAtAction(nameof(GetEmployeeById),
                new { id = employeeResponse.Id }, employeeResponse);
        }

        // GET: api/employee/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            return Ok(employee);
        }

        // GET: api/employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeResponse>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }
       [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return NoContent();
        }

        // PATCH: api/employee/{id}/status
        [HttpPut("change-status/{id}")]
        public async Task<IActionResult> ChangeStatus(int id, [FromBody] EmployeeStatus newStatus)
        {
            await _employeeService.ChangeEmployeeStatusAsync(id, newStatus);
            return Ok();
        }
        //login
        [HttpPost("login")]
        [ProducesResponseType(typeof(EmployeeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] EmployeeLoginRequest request)
        {
            try
            {
                var result = await _employeeService.LoginAsync(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
