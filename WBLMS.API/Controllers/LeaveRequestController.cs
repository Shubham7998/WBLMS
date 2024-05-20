using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WBLMS.DTO;
using WBLMS.IServices;
using WBLMS.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WBLMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly ILeaveRequestService _leaveRequestService;
        public LeaveRequestController(ILeaveRequestService leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
        }
        // GET: api/<LeaveRequest>
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<GetLeaveRequestDTO>>> GetAllLeaveRequests()
        //{
        //    try
        //    {
        //        var listOfLeaveRequests = await _leaveRequestService.GetAllLeaveRequests();
        //        if (listOfLeaveRequests != null)
        //        {
        //            return Ok(new
        //            {
        //                StatusCode = 200,
        //                data = listOfLeaveRequests
        //            });
        //        }
        //        return BadRequest(new
        //        {
        //            StatusCode = 400,
        //            ErrorMessage = "Invalid Request for all leave request!"
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new
        //        {
        //            StatusCode = 500,
        //            ErrorMessage = "Internal Server Error in leave request get by id api." + ex.Message
        //        });
        //        throw;

        //    }
        //}

        [HttpPost("paginated")]
        public async Task<IActionResult> GetAllLeaveRequestsPaginated(string? sortColumn, string? sortOrder, int page, int pageSize, GetLeaveRequestDTO leaveRequestObj)

        {
            try
            {
                var listOfLeaveRequestsTuple = await _leaveRequestService.GetAllLeaveRequests(sortColumn, sortOrder, page, pageSize, leaveRequestObj);
                if (listOfLeaveRequestsTuple.Item1 != null)
                {
                    var dataObj = new Paginated<GetLeaveRequestDTO>()
                    {
                        dataArray = listOfLeaveRequestsTuple.Item1,
                        totalPages = listOfLeaveRequestsTuple.Item2
                    };
                    return Ok(new
                    {
                        StatusCode = 200,
                        data = dataObj
                    });
                }
                return BadRequest(new
                {
                    StatusCode = 400,
                    ErrorMessage = "Invalid Request for all leave request!"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 500,
                    ErrorMessage = "Internal Server Error in leave request get by id api." + ex.Message
                });
                throw;

            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchAllLeaveRequests(int page, int pageSize, string search, long employeeId)

        {
            try
            {
                var listOfLeaveRequestsTuple = await _leaveRequestService.SearchLeaveRequests(page, pageSize, search, employeeId);
                if (listOfLeaveRequestsTuple.Item1 != null)
                {
                    var dataObj = new Paginated<GetLeaveRequestDTO>()
                    {
                        dataArray = listOfLeaveRequestsTuple.Item1,
                        totalPages = listOfLeaveRequestsTuple.Item2
                    };
                    return Ok(new
                    {
                        StatusCode = 200,
                        data = dataObj
                    });
                }
                return BadRequest(new
                {
                    StatusCode = 400,
                    ErrorMessage = "Invalid Request for all leave request!"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 500,
                    ErrorMessage = "Internal Server Error in leave request get by id api." + ex.Message
                });
                throw;

            }
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<GetLeaveRequestDTO>> GetLeaveRequestById(long id)
        {
            try
            {
                var leaveRequest = await _leaveRequestService.GetLeaveRequestById(id);
                if (leaveRequest != null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        data = leaveRequest
                    });
                }
                return BadRequest(new
                {
                    StatusCode = 400,
                    ErrorMessage = "Invalid Request for leave request by id!"
                });
            }
            catch (Exception ex)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    ErrorMessage = "Request of this id doesn't exist! " + ex.Message
                });
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<GetLeaveRequestDTO>> CreateLeaveRequest(CreateLeaveRequestDTO createLeaveRequestDTO)
        {
            try
            {
                var returnLeaveRequestObj = await _leaveRequestService.CreateLeaveRequest(createLeaveRequestDTO);
                if (returnLeaveRequestObj != null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        data = returnLeaveRequestObj
                    });
                }
                return BadRequest(new
                {
                    StatusCode = 400,
                    ErrorMessage = "Invalid Create Leave Request!."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 404,
                    ErrorMessage = "Create Leave Request Error.!" + ex.Message
                });
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetLeaveRequestDTO>> UpdateLeaveRequest(UpdateLeaveRequestDTO updateLeaveRequestDTO, long id)
        {
            try
            {
                if (updateLeaveRequestDTO.Id != id)
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        ErrorMessage = "UpdateLeaveRequestDTO Id and Provided Id doesn't match."
                    });
                }
                var returnLeaveRequestObj = await _leaveRequestService.UpdateLeaveRequest(updateLeaveRequestDTO);
                if (returnLeaveRequestObj != null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        data = returnLeaveRequestObj
                    });
                }
                return BadRequest(new
                {
                    StatusCode = 400,
                    ErrorMessage = "Invalid Update Leave Request!."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 404,
                    ErrorMessage = "Update Leave Request Error.!" + ex.Message
                });
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteLeaveRequest(long id)
        {
            try
            {
                var result = await _leaveRequestService.DeleteLeaveRequest(id);
                return Ok(new
                {
                    StatusCode = 200,
                    data = result ? "Data of Id: " + id + " Delete Successfully" : "Data of Id" + id + " Delete Unsuccessfully"
                });
            }
            catch (Exception ex)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    ErrorMessage = "Leave Request doesn't exist. " + ex.Message
                });
                throw;
            }
        }

        [HttpGet("leavesBalance/{id}")]
        public async Task<ActionResult<GetLeavesBalanceDTO>> GetLeavesBalanceByEmployeeId(long id)
        {
            try
            {
                if (id > 0)
                {
                    var leaveBalance = await _leaveRequestService.GetLeavesBalanceById(id);
                    return Ok(new
                    {
                        StatusCode = 200,
                        data = leaveBalance
                    });
                }
                return BadRequest(new
                {
                    StatusCode = 400,
                    ErrorMessage = "Invalid GetLeavesBalance request!"
                });
            }
            catch (Exception)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    ErrorMessage = "Data of EmployeeId: " + id + " doesn't exist."
                });
                throw;
            }
        }
        [HttpGet("leavetype")]
        public async Task<ActionResult<IEnumerable<GetLeaveTypesDTO>>> GetLeaveTypes()
        {
            try
            {
                var leaveTypes = await _leaveRequestService.GetLeavesType();
                return Ok(new
                {
                    StatusCode = 200,
                    data = leaveTypes
                });
            }
            catch (Exception)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    ErrorMessage = "Leave Types are empty"
                });
                throw;
            }
        }
    }
}
