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

        [HttpPost("paginated")]
        public async Task<ActionResult<Paginated<GetEmployeeDTO>>> GetPaginated(int page, int pageSize, string? sortColumn, string? sortOrder, GetEmployeeDTO employee)
        {
            try
            {
                var result = await _employeeService.GetAllEmployeeAsync(page, pageSize, sortColumn, sortOrder, employee);

                var sendResult = new Paginated<GetEmployeeDTO>
                {
                    dataArray = result.Item1,
                    totalPages = result.Item2,
                };
                return Ok(new
                {
                    StatusCode = 200,
                    data = sendResult
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 500,
                    ErrorMessage = "Internal Server Error " + ex.Message
                });
            }
        }
        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetEmployeeDTO>> Get(int id)
        {
            try
            {
                var result = await _employeeService.GetEmployeeByIdAsync(id);
                if(result != null)
                {
                    return Ok(new
                    {
                        StatusCode=200,
                        data = result
                    });
                }
                return BadRequest(new
                {
                    StatusCode = 204,
                    ErrorMessage = "Data with id is not present"
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 500,
                    ErrorMessage = "Internal Server Error " + ex.Message
                });
            }
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<ActionResult<GetEmployeeDTO>> Post([FromBody] CreateEmployeeDTO createEmployeeDTO)
        {
            try
            {
                var result = await _employeeService.CreateEmployeeAsync(createEmployeeDTO);
                if (result != null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        data = result
                    });
                }
                return Ok(new
                {
                    StatusCode = 204,
                    data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 500,
                    ErrorMessage = "Internal Server Error " + ex.Message
                });
            }
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<GetEmployeeDTO>> Put(int id, [FromBody] UpdateEmployeeDTO employeeDTO)
        {
            try
            {
                var result = await _employeeService.UpdateEmployeeAsync(employeeDTO, id);
                if (result != null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        data = result
                    });
                }
                return Ok(new
                {
                    StatusCode = 204,
                    data = result
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 500,
                    ErrorMessage = "Internal Server Error " + ex.Message
                });
            }
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                var result = await _employeeService.DeleteEmployeeAsync(id);
                if (result != null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        data = result
                    });
                }
                return BadRequest(new
                {
                    StatusCode = 204,
                    ErrorMessage = "Data with id is not present"
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 500,
                    ErrorMessage = "Internal Server Error " + ex.Message
                });
            }
        }
    }
}
