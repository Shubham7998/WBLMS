using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WBLMS.DTO;
using WBLMS.IRepositories;
using WBLMS.IServices;
using WBLMS.Models;
using WBLMS.Utilities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WBLMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly JwtSettings _jwtSettings;

        public EmployeeController(IEmployeeService employeeService, IOptions<JwtSettings> jwtSettings)
        {
            _employeeService = employeeService;
            _jwtSettings = jwtSettings.Value;
        }

        [Authorize(Roles ="Admin,HR,Team Lead")]
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
        [Authorize(Roles ="Admin,HR,Team Lead")]
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
        [Authorize(Roles = "Admin")]

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
        [Authorize(Roles = "Admin")]

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
        [Authorize(Roles = "Admin")]

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var jwt = _jwtSettings.Key;
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
