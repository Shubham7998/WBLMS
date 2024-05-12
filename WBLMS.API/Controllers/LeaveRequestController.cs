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
                ErrorMessage = "Invalid request from controller!"
            });
        }
    } 
}
