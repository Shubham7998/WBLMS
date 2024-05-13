using Microsoft.AspNetCore.Mvc;
using WBLMS.DTO;
using WBLMS.IRepositories;
using WBLMS.IServices;
using WBLMS.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WBLMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetEmployeeDTO>>> Get(int page, int pageSize, string? sortColumn, string? sortOrder, GetEmployeeDTO employee)
        {
            try
            {
                var result = await _employeeService.GetAllEmployeeAsync(page,pageSize,sortColumn,sortOrder,employee);
                return Ok(result);
            }catch (Exception ex)
            {
                throw;
            }
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetEmployeeDTO>> Get(int id)
        {
            try
            {
                var result = await _employeeService.GetEmployeeByIdAsync(id);
                return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<ActionResult<GetEmployeeDTO>> Post([FromBody] CreateEmployeeDTO createEmployeeDTO)
        {
            try
            {
                var result = await _employeeService.CreateEmployeeAsync(createEmployeeDTO);
                return Ok(result);  
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<GetEmployeeDTO>> Put(int id, [FromBody] UpdateEmployeeDTO employeeDTO)
        {
            try
            {
                var result = await _employeeService.UpdateEmployeeAsync(employeeDTO, id);
                return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                var result = await _employeeService.DeleteEmployeeAsync(id);
                return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //public Task<>
    }
}
