using WBLMS.DTO;
using WBLMS.IRepositories;
using WBLMS.IServices;
using WBLMS.Models;
namespace WBLMS.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository, IEmployeeRepository employeeRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;   
            _employeeRepository = employeeRepository;
        }

        public async Task<CreateLeaveRequestDTO> CreateLeaveRequest(CreateLeaveRequestDTO leaveRequestDTO)
        {
            if(leaveRequestDTO != null) 
            {
                // Get Manager From the table using Employee Id to get manager Id


                var newLeaveRequestObj = new LeaveRequest()
                {
                    EmployeeId = leaveRequestDTO.EmployeeId,
                    LeaveTypeId = leaveRequestDTO.LeaveTypeId,
                    Reason = leaveRequestDTO.Reason,
                    // Default is Pending so 1
                    StatusId = 1,
                    ManagerId = 0,
                    StartDate = leaveRequestDTO.StartDate,
                    EndDate = leaveRequestDTO.EndDate,
                    NumberOfLeaveDays = leaveRequestDTO.NumberOfLeaveDays,
                    // ApprovedDate 

                    // RequestDate is set to current Date
                    RequestDate = DateOnly.FromDateTime(DateTime.UtcNow.Date)
                };
                return leaveRequestDTO;
            }
            return null;
        }

        public async Task<IEnumerable<GetLeaveRequestDTO>> GetAllLeaveRequests()
        {
            var listOfLeaveRequests = await _leaveRequestRepository.GetAllLeaveRequests();

            if (listOfLeaveRequests != null)
            {
                var listOfLeaveRequestDTO = listOfLeaveRequests
                    .Select(request => new GetLeaveRequestDTO(
                        request.Id, request.EmployeeId, request.Employee.FirstName, request.Employee.LastName, request.LeaveType.LeaveTypeName, request.Reason, request.Status.StatusName, request.StartDate, request.EndDate, request.NumberOfLeaveDays, request.RequestDate, request.ApprovedDate
                    )
                );
                return listOfLeaveRequestDTO;
            }
            return null;
        }

        public async Task<GetLeaveRequestDTO> GetLeaveRequestById(long id)
        {
            var leaveRequest = await _leaveRequestRepository.GetLeaveRequestById(id);
            if ( leaveRequest != null )
            {
                var leaveRequestDTO = new GetLeaveRequestDTO(
                        leaveRequest.Id, leaveRequest.EmployeeId, leaveRequest.Employee.FirstName, leaveRequest.Employee.LastName,
                        leaveRequest.LeaveType.LeaveTypeName, leaveRequest.Reason, leaveRequest.Status.StatusName, leaveRequest.StartDate,
                        leaveRequest.EndDate, leaveRequest.NumberOfLeaveDays, leaveRequest.RequestDate, leaveRequest.ApprovedDate   
                    );
                return leaveRequestDTO;
            }
            return null;
        }
    }
}
