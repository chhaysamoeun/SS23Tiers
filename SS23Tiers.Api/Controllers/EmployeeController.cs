using Microsoft.AspNetCore.Mvc;
using SS23Tiers.Api.Models;
using SS23Tiers.Api.Service;

namespace SS23Tiers.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employee;

    public EmployeeController(IEmployeeService employee)
    {
        _employee = employee;
    }
    // GET: api/Employee
    [HttpGet]
    public async Task<IEnumerable<Employee>> Get()
        => await _employee.GetEmployees();

    // GET: api/Employee/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> Get(Guid id)
        => await _employee.GetEmployee(id);
    // POST: api/Employee
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Employee employee)
    {
        if (ModelState.IsValid)
        {
            if(await _employee.Save(employee))
            {
                return Created("CreateEmployee", employee);
            }
        }
        return BadRequest(ModelState);
    }

    // PUT: api/Employee/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] Employee employee)
    {
        if (ModelState.IsValid || id==employee.EmployeeId)
        {
            if (await _employee.Update(employee,id))
            {
                return Ok();
            }
        }
        return BadRequest(ModelState);
    }

    // DELETE: api/Employee/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        if(await _employee.Delete(id))
        {
            return Ok();
        }
        return BadRequest();
    }
}
