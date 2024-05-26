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

        string successMessage = "Successfull";
       
        [HttpPost("paginated")]
        public async Task<IActionResult> GetAllLeaveRequestsPaginated(string? sortColumn, string? sortOrder, int page, int pageSize, GetLeaveRequestDTO leaveRequestObj)

        {
            try
            {
                var listOfLeaveRequestsTuple = await _leaveRequestService.GetAllLeaveRequests(sortColumn, sortOrder, page, pageSize, leaveRequestObj, "");
                if (listOfLeaveRequestsTuple.Item1 != null)
                {
                    var dataObj = new Paginated<GetLeaveRequestDTO>()
                    {
                        dataArray = listOfLeaveRequestsTuple.Item1,
                        totalCount = listOfLeaveRequestsTuple.Item2
                    };
                    //return Ok(new
                    //{
                    //    StatusCode = 200,
                    //    data = dataObj
                    //});
                    return Ok(new APIResponseDTO<Paginated<GetLeaveRequestDTO>>(200, dataObj, successMessage));
                    //{
                    //    StatusCode = 200,
                    //    data = dataObj,
                    //    ErrorMessages = ""
                    //});
                }
                //return BadRequest(new
                //{
                //    StatusCode = 400,
                //    ErrorMessage = "Invalid Request for all leave request!"
                //});
                return NotFound(new APIResponseDTO<GetLeaveRequestDTO>(404, null, "Invalid Leave Request"));
                //{
                //    StatusCode = 404,
                //    data = null,
                //    ErrorMessages = "Invalid Leave Request"
                //});
            }
            catch (Exception ex)
            {
                //return BadRequest(new
                //{
                //    StatusCode = 500,
                //    ErrorMessage = "Internal Server Error in leave request get by id api." + ex.Message
                //});
                //throw;
                return BadRequest(new APIResponseDTO<GetLeaveRequestDTO>(500, null, ex.Message));
                //{
                //    StatusCode = 500,
                //    data = null,
                //    ErrorMessages = ex.Message
                //});

            }
        }
        
        [HttpPost("byRoles")]
        public async Task<IActionResult> GetAllRolesRequestsPaginated(string? sortColumn, string? sortOrder, int page, int pageSize, GetLeaveRequestDTO leaveRequestObj, string? searchKeyword)

        {
            try
            {
                var listOfLeaveRequestsTuple = await _leaveRequestService.GetAllLeaveRequests(sortColumn, sortOrder, page, pageSize, leaveRequestObj, searchKeyword);
                if (listOfLeaveRequestsTuple.Item1 != null)
                {
                    var dataObj = new Paginated<GetLeaveRequestDTO>()
                    {
                        dataArray = listOfLeaveRequestsTuple.Item1,
                        totalCount = listOfLeaveRequestsTuple.Item2
                    };
                    return Ok(new APIResponseDTO<Paginated<GetLeaveRequestDTO>>(200, dataObj, successMessage));

                }
                //return BadRequest(new
                //{
                //    StatusCode = 400,
                //    ErrorMessage = "Invalid Request for all leave request!"
                //});
                return Ok(new APIResponseDTO<Paginated<GetLeaveRequestDTO>>(200, null, "Invalid Request for all leave request!"));

            }
            catch (Exception ex)
            {
                //return BadRequest(new
                //{
                //    StatusCode = 500,
                //    ErrorMessage = "Internal Server Error in leave request get by id api." + ex.Message
                //});
                return Ok(new APIResponseDTO<Paginated<GetLeaveRequestDTO>>(500, null, ex.Message));

                //throw;

            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchAllLeaveRequests(int page, int pageSize, string? search, long employeeId, long managerId)

        {
            try
            {
                var listOfLeaveRequestsTuple = await _leaveRequestService.SearchLeaveRequests(page, pageSize, search, employeeId, managerId);
                if (listOfLeaveRequestsTuple.Item1 != null)
                {
                    var dataObj = new Paginated<GetLeaveRequestDTO>()
                    {
                        dataArray = listOfLeaveRequestsTuple.Item1,
                        totalCount = listOfLeaveRequestsTuple.Item2
                    };
                    //return Ok(new
                    //{
                    //    StatusCode = 200,
                    //    data = dataObj
                    //});
                    return Ok(new APIResponseDTO<Paginated<GetLeaveRequestDTO>>(200, dataObj, successMessage));

                }
                //return BadRequest(new
                //{
                //    StatusCode = 400,
                //    ErrorMessage = "Invalid Request for all leave request!"
                //});
                return Ok(new APIResponseDTO<Paginated<GetLeaveRequestDTO>>(200, null, ""));

            }
            catch (Exception ex)
            {
                //return BadRequest(new
                //{
                //    StatusCode = 500,
                //    ErrorMessage = "Internal Server Error in leave request get by id api." + ex.Message
                //});
                return Ok(new APIResponseDTO<Paginated<GetLeaveRequestDTO>>(500, null, "Internal Server Error in leave request get by id api." + ex.Message));

                //throw;

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
                    //return Ok(new
                    //{
                    //    StatusCode = 200,
                    //    data = leaveRequest
                    //});
                    return Ok(new APIResponseDTO<GetLeaveRequestDTO>(200, leaveRequest, successMessage));

                }
                //return BadRequest(new
                //{
                //    StatusCode = 400,
                //    ErrorMessage = "Invalid Request for leave request by id!"
                //});
                return Ok(new APIResponseDTO<GetLeaveRequestDTO>(200, null, "Invalid Request for leave request by id!"));

            }
            catch (Exception ex)
            {
                //return NotFound(new
                //{
                //    StatusCode = 404,
                //    ErrorMessage = "Request of this id doesn't exist! " + ex.Message
                //});
                return Ok(new APIResponseDTO<GetLeaveRequestDTO>(200, null, "Request of this id doesn't exist! " + ex.Message));
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
                    //return Ok(new
                    //{
                    //    StatusCode = 200,
                    //    data = returnLeaveRequestObj
                    //});
                    return Ok(new APIResponseDTO<GetLeaveRequestDTO>(200, returnLeaveRequestObj, successMessage));
                }
                //return BadRequest(new
                //{
                //    StatusCode = 400,
                //    ErrorMessage = "Invalid Create Leave Request!."
                //});
                return Ok(new APIResponseDTO<GetLeaveRequestDTO>(400, returnLeaveRequestObj, "Invalid Create Leave Request!."));

            }
            catch (Exception ex)
            {
                return Ok(new APIResponseDTO<GetLeaveRequestDTO>(500, null, "Invalid Create Leave Request!."));
                //return BadRequest(new
                //{
                //    StatusCode = 404,
                //    ErrorMessage = "Create Leave Request Error.!" + ex.Message
                //});
                //throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetLeaveRequestDTO>> UpdateLeaveRequest(UpdateLeaveRequestDTO updateLeaveRequestDTO, long id)
        {
            try
            {
                if (updateLeaveRequestDTO.Id != id)
                {
                    //return BadRequest(new
                    //{
                    //    StatusCode = 400,
                    //    ErrorMessage = "UpdateLeaveRequestDTO Id and Provided Id doesn't match."
                    //});
                    return Ok(new APIResponseDTO<GetLeaveRequestDTO>(500, null, "UpdateLeaveRequestDTO Id and Provided Id doesn't match."));

                }
                var returnLeaveRequestObj = await _leaveRequestService.UpdateLeaveRequest(updateLeaveRequestDTO);
                if (returnLeaveRequestObj != null)
                {
                    //return Ok(new
                    //{
                    //    StatusCode = 200,
                    //    data = returnLeaveRequestObj
                    //});
                    return Ok(new APIResponseDTO<GetLeaveRequestDTO>(200, returnLeaveRequestObj, successMessage));

                }
                //return BadRequest(new
                //{
                //    StatusCode = 400,
                //    ErrorMessage = "Invalid Update Leave Request!."
                //}); 
                return Ok(new APIResponseDTO<GetLeaveRequestDTO>(400, null, "Invalid Update Leave Request!"));

            }
            catch (Exception ex)
            {
                //return BadRequest(new
                //{
                //    StatusCode = 404,
                //    ErrorMessage = "Update Leave Request Error.!" + ex.Message
                //});
                //throw;
                return Ok(new APIResponseDTO<GetLeaveRequestDTO>(500, null, ex.Message));

            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteLeaveRequest(long id)
        {
            try
            {
                var result = await _leaveRequestService.DeleteLeaveRequest(id);
                //return Ok(new
                //{
                //    StatusCode = 200,
                //    data = result ? "Data of Id: " + id + " Delete Successfully" : "Data of Id" + id + " Delete Unsuccessfully"
                //});
                return NotFound(new APIResponseDTO<string>(200, result ? "Data of Id: " + id + " Delete Successfully" : "Data of Id" + id + " Delete Unsuccessfully", "true"));

            }
            catch (Exception ex)
            {
                //return NotFound(new
                //{
                //    StatusCode = 404,
                //    ErrorMessage = "Leave Request doesn't exist. " + ex.Message
                //});
                //throw;
                return NotFound(new APIResponseDTO<GetCountOfLeaveStatusesDTO>(500, null, ex.Message));

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
                    //return Ok(new
                    //{
                    //    StatusCode = 200,
                    //    data = leaveBalance
                    //});
                    return Ok(new APIResponseDTO<GetLeavesBalanceDTO>(200, leaveBalance, successMessage));

                }
                //return BadRequest(new
                //{
                //    StatusCode = 400,
                //    ErrorMessage = "Invalid GetLeavesBalance request!"
                //});
                return Ok(new APIResponseDTO<GetLeaveRequestDTO>(500, null, "Data with this id is not present"));

            }
            catch (Exception ex)
            {
                return NotFound(new APIResponseDTO<GetCountOfLeaveStatusesDTO>(500, null, ex.Message));

                return NotFound(new
                {
                    StatusCode = 404,
                    ErrorMessage = "Data of EmployeeId: " + id + " doesn't exist."
                });
                throw;
            }
        }

        [HttpGet("getLeaveBy/{year}")]
        public async Task<ActionResult<GetLeaveRequestByYear>> GetLeavesReqByYear(long year)
        {
            try
            {
                var leaveCount = await _leaveRequestService.GetLeaveRequestCountByYear(year);
                if(leaveCount != null)
                {
                    return Ok(new APIResponseDTO<GetLeaveRequestByYear>(200, leaveCount, successMessage));

                }
                    return Ok(new APIResponseDTO<GetLeaveRequestByYear>(204, null, "No data found"));

            }
            catch (Exception ex)
            {
                return NotFound(new APIResponseDTO<GetLeaveRequestByYear>(500, null, ex.Message));
            }
        }
        [HttpGet("leavetype")]
        public async Task<ActionResult<IEnumerable<GetLeaveTypesDTO>>> GetLeaveTypes()
        {
            try
            {
                var leaveTypes = await _leaveRequestService.GetLeavesType();
                return Ok(new APIResponseDTO<IEnumerable<GetLeaveTypesDTO>>(200, leaveTypes, successMessage));

                return Ok(new
                {
                    StatusCode = 200,
                    data = leaveTypes
                });
            }
            catch (Exception ex)
            {
                return NotFound(new APIResponseDTO<GetCountOfLeaveStatusesDTO>(500, null, ex.Message));

                //return NotFound(new
                //{
                //    StatusCode = 404,
                //    ErrorMessage = "Leave Types are empty"
                //});
                //throw;
            }
        }
        [HttpGet("leavesStatusesData/{id}")]
        public async Task<ActionResult<GetCountOfLeaveStatusesDTO>> GetLeaveStatusesData(long id)
        {
            try
            {
                var leaveStatusesData = await _leaveRequestService.GetCountOfLeaveStatuses(id);
                if (leaveStatusesData != null)
                {
                    //return Ok(new
                    //{
                    //    StatusCode = 200,
                    //    data = leaveStatusesData
                    //});
                    return Ok(new APIResponseDTO<GetCountOfLeaveStatusesDTO>(200, leaveStatusesData, successMessage));

                }
                //return NotFound(new
                //{
                //    StatusCode = 404,
                //    ErrorMessage = "No Leaves Exist"
                //});
                return Ok(new APIResponseDTO<GetLeaveRequestDTO>(404, null, "No Leaves Exist."));

            }
            catch (Exception ex)
            {
                return NotFound(new APIResponseDTO<GetCountOfLeaveStatusesDTO>(500, null, "Error occur"));
                throw;
            }
        }

        [HttpGet("wbHolidays")]

        public async Task<ActionResult<IEnumerable<GetWonderbizLeaveDTO>>> GetWBHolidays()
        {
            try
            {
                var holidays = await _leaveRequestService.GetWonderbizHolidays();
                //return Ok(new
                //{
                //    StatusCode = 200,
                //    data = holidays
                //});
                return Ok(new APIResponseDTO<IEnumerable<GetWonderbizLeaveDTO>>(200, holidays, successMessage));

            }
            catch (Exception ex)
            {
                //return NotFound(new
                //{
                //    StatusCode = 404,
                //    ErrorMessage = "No holiday"
                //});
                //throw;
                return NotFound(new APIResponseDTO<GetLeaveRequestDTO>(500, null, "Error occur"));

            }
        }
    }
}
