using WBLMS.DTO;
using WBLMS.IRepositories;
using WBLMS.Models;

namespace WBLMS.IServices
{
    public interface ILeaveRequestService
    {
        Task<IEnumerable<GetLeaveRequestDTO>> GetAllLeaveRequests();
        Task<GetLeaveRequestDTO> GetLeaveRequestById(long id);
        Task<GetLeaveRequestDTO> CreateLeaveRequest(CreateLeaveRequestDTO leaveRequestDTO);
        Task<GetLeaveRequestDTO> UpdateLeaveRequest(UpdateLeaveRequestDTO leaveRequestDTO);
    }
}
