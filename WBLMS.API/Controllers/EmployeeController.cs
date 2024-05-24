﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Data;
using System.IO.Pipelines;
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
        string success = "Successfull";

        [HttpGet("gender")]
        public async Task<ActionResult> GetGenders()
        {
            try
            {
                var genders = await _employeeService.GetAllGenderAsync();
                //return Ok(new
                //{
                //    StatusCode = 200,
                //    data = genders
                //});
                return Ok(new APIResponseDTO<IEnumerable<Gender>> (200, genders, success));
            }
            catch (Exception ex) 
            {
                return NotFound(new APIResponseDTO<EmptyResult>(500, null, ex.Message));
            }
        }

        [HttpGet("manager/{id}")]

        public async Task<ActionResult> GetManagers(long id)
        {
            try
            {
                var managers = await _employeeService.GetAllManagersAsync(id);
                //return Ok(new
                //{
                //    StatusCode = 200,
                //    data = managers
                //});
                return Ok(new APIResponseDTO<IEnumerable<GetManagerDTO>>(200, managers, success));

            }
            catch (Exception ex)
            {
                return NotFound(new APIResponseDTO<EmptyResult>(500, null, ex.Message));
            }
        }

        [HttpGet("roles")]
        public async Task<ActionResult> GetRoles()
        {
            try
            {
                var roles = await _employeeService.GetAllRolesAsync();
                //return Ok(new
                //{
                //    StatusCode = 200,
                //    data = roles
                //});
                return Ok(new APIResponseDTO<IEnumerable<Roles>>(200, roles, success));
            }
            catch (Exception ex)
            {
                return NotFound(new APIResponseDTO<EmptyResult>(500, null, ex.Message));
            }
        }

        [Authorize(Roles = "Admin,HR,Team Lead")]
        [HttpPost("paginated")]
        public async Task<ActionResult<Paginated<GetEmployeeForeignDTO>>> GetPaginated(int page, int pageSize, string? sortColumn, string? sortOrder, GetEmployeeDTO employee)
        {
            try
            {
                var result = await _employeeService.GetAllEmployeeForeignAsync(page, pageSize, sortColumn, sortOrder, employee);

                var sendResult = new Paginated<GetEmployeeForeignDTO>
                {
                    dataArray = result.Item1,
                    totalCount = result.Item2,
                };
                //return Ok(new
                //{
                //    StatusCode = 200,
                //    data = sendResult
                //});
                return Ok(new APIResponseDTO<Paginated<GetEmployeeForeignDTO>>(200, sendResult, success));

            }
            catch (Exception ex)
            {
                return NotFound(new APIResponseDTO<EmptyResult>(500, null, ex.Message));
            }
        }
        // GET api/<EmployeeController>/5
        //[Authorize(Roles = "Admin,HR,Team Lead")]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetEmployeeForeignDTO>> Get(int id)
        {
            try
            {
                var result = await _employeeService.GetEmployeeForeignByIdAsync(id);
                if(result != null)
                {
                    //return Ok(new
                    //{
                    //    StatusCode=200,
                    //    data = result
                    //});
                    return Ok(new APIResponseDTO<GetEmployeeForeignDTO>(200, result, success));

                }
                //return BadRequest(new
                //{
                //    StatusCode = 204,
                //    ErrorMessage = "Data with id is not present"
                //});
                return Ok(new APIResponseDTO<GetEmployeeForeignDTO>(204, null, "Data with id is not present"));

            }
            catch (Exception ex)
            {
                return NotFound(new APIResponseDTO<EmptyResult>(500, null, ex.Message));
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
                    //return Ok(new
                    //{
                    //    StatusCode = 200,
                    //    data = result
                    //});
                    return Ok(new APIResponseDTO<GetEmployeeDTO>(200, result, success));
                }
                //return Ok(new
                //{
                //    StatusCode = 204,
                //    data = result
                //});
                return Ok(new APIResponseDTO<GetEmployeeDTO>(200, result, success));

            }
            catch (Exception ex)
            {
                return NotFound(new APIResponseDTO<EmptyResult>(500, null, ex.Message));
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
                    //return Ok(new
                    //{
                    //    StatusCode = 200,
                    //    data = result
                    //});
                return Ok(new APIResponseDTO<GetEmployeeDTO>(200, result, success));
                }
                //return Ok(new
                //{
                //    StatusCode = 204,
                //    data = result
                //});
                return Ok(new APIResponseDTO<GetEmployeeDTO>(404, null, success));
            }
            catch (Exception ex)
            {
                return NotFound(new APIResponseDTO<EmptyResult>(500, null, ex.Message));
            }
        }

        // DELETE api/<EmployeeController>/5
        [Authorize(Roles = "Admin")]

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                var result = await _employeeService.DeleteEmployeeAsync(id);
                var msg = result ? "Deleted successfully" : "Deletion unsuccessful";
                if (result != null)
                {
                    //return Ok(new
                    //{
                    //    StatusCode = 200,
                    //    data = result
                    //});
                    return Ok(new APIResponseDTO<string>(200, msg , success));

                }
                //return BadRequest(new
                //{
                //    StatusCode = 204,
                //    ErrorMessage = "Data with id is not present"
                //});
                return Ok(new APIResponseDTO<EmptyResult>(400, null, success));

            }
            catch (Exception ex)
            {
                return NotFound(new APIResponseDTO<EmptyResult>(500, null, ex.Message));
            }
        }

    }
}
