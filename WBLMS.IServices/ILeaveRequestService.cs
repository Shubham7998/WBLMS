using WBLMS.DTO;
using WBLMS.IRepositories;
using WBLMS.Models;

namespace WBLMS.IServices
{
    public interface ILeaveRequestService
    {
        Task<IEnumerable<GetLeaveRequestDTO>> GetAllLeaveRequests();
    }
}
