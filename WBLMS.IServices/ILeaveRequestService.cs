using WBLMS.DTO;
using WBLMS.IRepositories;
using WBLMS.Models;

namespace WBLMS.IServices
{
    public interface ILeaveRequestService
    {
        Task<(IEnumerable<GetLeaveRequestDTO>, int)> GetAllLeaveRequests(string? sortColumn, string? sortOrder, int page, int pageSize, GetLeaveRequestDTO leaveRequestObj, string? searchKeyword);
        Task<GetLeaveRequestDTO> GetLeaveRequestById(long id);
        Task<GetLeaveRequestDTO> CreateLeaveRequest(CreateLeaveRequestDTO leaveRequestDTO);
        Task<GetLeaveRequestDTO> UpdateLeaveRequest(UpdateLeaveRequestDTO leaveRequestDTO);
        Task<bool> DeleteLeaveRequest(long id);
        Task<GetLeavesBalanceDTO> GetLeavesBalanceById(long employeeId);

        Task<IEnumerable<GetWonderbizLeaveDTO>> GetWonderbizHolidays();

        Task<(IEnumerable<GetLeaveRequestDTO>, int)> SearchLeaveRequests(int page, int pageSize, string? search, long employeeId, long managerId);

        Task<IEnumerable<GetLeaveTypesDTO>> GetLeavesType();
        Task<GetCountOfLeaveStatusesDTO> GetCountOfLeaveStatuses(long employeeId);
        Task<GetLeaveRequestByYear> GetLeaveRequestCountByYear(long  year);
    }
}