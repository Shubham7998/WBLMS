using WBLMS.DTO;
using WBLMS.IRepositories;
using WBLMS.Models;

namespace WBLMS.IServices
{
    public interface ILeaveRequestService
    {
        Task<(IEnumerable<GetLeaveRequestDTO>, int)> GetAllLeaveRequests(string? sortColumn, string? sortOrder, int page, int pageSize, GetLeaveRequestDTO leaveRequestObj);
        Task<GetLeaveRequestDTO> GetLeaveRequestById(long id);
        Task<GetLeaveRequestDTO> CreateLeaveRequest(CreateLeaveRequestDTO leaveRequestDTO);
        Task<GetLeaveRequestDTO> UpdateLeaveRequest(UpdateLeaveRequestDTO leaveRequestDTO);
        Task<bool> DeleteLeaveRequest(long id);
    }
}
