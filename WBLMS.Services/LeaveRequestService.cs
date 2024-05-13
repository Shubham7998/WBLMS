using WBLMS.DTO;
using WBLMS.IRepositories;
using WBLMS.IServices;
using WBLMS.Models;
namespace WBLMS.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;   
        }

        public async Task<GetLeaveRequestDTO> CreateLeaveRequest(CreateLeaveRequestDTO leaveRequestDTO)
        {
            if(leaveRequestDTO != null) 
            {
                // Get Manager From the table using Employee Id to get manager Id
                // Calculate Number of leave days

                var newLeaveRequestObj = new LeaveRequest()
                {
                    EmployeeId = leaveRequestDTO.EmployeeId,
                    LeaveTypeId = leaveRequestDTO.LeaveTypeId,
                    Reason = leaveRequestDTO.Reason,
                    // Default is Pending so 1
                    StatusId = 1,
                    ManagerId = leaveRequestDTO.ManagerId,
                    StartDate = leaveRequestDTO.StartDate,
                    EndDate = leaveRequestDTO.EndDate,
                    // Calculate Number of working days from start date and end date
                    NumberOfLeaveDays = GetBuisnessDays(leaveRequestDTO.StartDate, leaveRequestDTO.EndDate, leaveRequestDTO.isHalfDay),
                    // ApprovedDate 

                    // RequestDate is set to current Date
                    RequestDate = DateOnly.FromDateTime(DateTime.UtcNow.Date)
                };
                var returnObj = await _leaveRequestRepository.CreateLeaveRequest(newLeaveRequestObj);
                var returnObjDTO = new GetLeaveRequestDTO(
                        returnObj.Id, returnObj.EmployeeId, returnObj.Employee.FirstName, returnObj.Employee.LastName,
                        returnObj.LeaveType.LeaveTypeName, returnObj.Reason, returnObj.Status.StatusName, returnObj.StartDate,
                        returnObj.EndDate, returnObj.NumberOfLeaveDays, returnObj.RequestDate, returnObj.ApprovedDate
                    );
                return returnObjDTO;
            }
            return null;
        }

        public async Task<(IEnumerable<GetLeaveRequestDTO>, int)> GetAllLeaveRequests(string? sortColumn, string? sortOrder, int page, int pagesize, LeaveRequest leaveRequestObj)
        {
            var listOfLeaveRequestsTuple = await _leaveRequestRepository.GetAllLeaveRequests(sortColumn, sortOrder, page, pagesize, leaveRequestObj);

            if (listOfLeaveRequestsTuple.Item1 != null)
            {
                var listOfLeaveRequestDTO = listOfLeaveRequestsTuple
                    .Item1
                    .Select(request => new GetLeaveRequestDTO(
                        request.Id, request.EmployeeId, request.Employee.FirstName, request.Employee.LastName, request.LeaveType.LeaveTypeName, request.Reason, request.Status.StatusName, request.StartDate, request.EndDate, request.NumberOfLeaveDays, request.RequestDate, request.ApprovedDate
                    )
                );
                return (listOfLeaveRequestDTO, listOfLeaveRequestsTuple.Item2);
            }
            return (null, 0);
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
        public decimal GetBuisnessDays(DateOnly StartDate, DateOnly EndDate, bool isHalfDay)
        {
            int counter = 0;

            if (StartDate == EndDate)
            {
                if (StartDate.DayOfWeek != DayOfWeek.Saturday && StartDate.DayOfWeek != DayOfWeek.Friday)
                {   if(isHalfDay)
                    {
                        return 0.5M;
                    }
                    return 1;

                }
                return 0;
            }

            while (StartDate <= EndDate)
            {
                if (StartDate.DayOfWeek != DayOfWeek.Saturday && StartDate.DayOfWeek != DayOfWeek.Friday)
                    ++counter;
                StartDate = StartDate.AddDays(1);
            }

            return counter;
        }

        public async Task<GetLeaveRequestDTO> UpdateLeaveRequest(UpdateLeaveRequestDTO leaveRequestDTO)
        {
            var oldLeaveRequest = await _leaveRequestRepository.GetLeaveRequestById(leaveRequestDTO.Id);
            
            if(oldLeaveRequest != null)
            {
                // Updating status 
                oldLeaveRequest.StatusId = leaveRequestDTO.StatusId;

                // If Status is Approved then we Update Balance on the LeaveBalances Table
                // Need to implement that

                // Storing the Updated Obj
                var returnObj = await _leaveRequestRepository.UpdateLeaveRequest(oldLeaveRequest);

                var returnObjDTO = new GetLeaveRequestDTO(
                        returnObj.Id, returnObj.EmployeeId, returnObj.Employee.FirstName, returnObj.Employee.LastName,
                        returnObj.LeaveType.LeaveTypeName, returnObj.Reason, returnObj.Status.StatusName, returnObj.StartDate,
                        returnObj.EndDate, returnObj.NumberOfLeaveDays, returnObj.RequestDate, returnObj.ApprovedDate
                    );
                return returnObjDTO;
            }
            return null;

        }

        public async Task<bool> DeleteLeaveRequest(long id)
        {
            try
            {
                var objToBeDeleted = await _leaveRequestRepository.GetLeaveRequestById(id);

                return await _leaveRequestRepository.DeleteAsync(objToBeDeleted);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
