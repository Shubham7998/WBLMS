using WBLMS.DTO;
using WBLMS.Repositories;

namespace WBLMS.Services
{
    public class LeaveRequestService
    {
        private readonly LeaveRequestRepository _leaveRequestRepository;
        public LeaveRequestService(LeaveRequestRepository leaveRequestRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;   
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
    }
}
