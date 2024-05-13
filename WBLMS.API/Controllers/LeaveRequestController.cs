using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WBLMS.DTO;
using WBLMS.IServices;

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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetLeaveRequestDTO>>> GetAllLeaveRequests()
        {
            try
            {
                var listOfLeaveRequests = await _leaveRequestService.GetAllLeaveRequests();
                if (listOfLeaveRequests != null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        data = listOfLeaveRequests
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

        [HttpGet("id")]
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
    } 
}
