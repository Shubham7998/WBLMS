using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBLMS.Models;

namespace WBLMS.IRepositories
{
    public interface ILeaveRequestRepository : IRepository<LeaveRequest>
    {
        Task<IEnumerable<LeaveRequest>> GetAllLeaveRequests();
        Task<LeaveRequest> GetLeaveRequestById(long id);
        Task<LeaveRequest> CreateLeaveRequest(LeaveRequest leaveRequest);
        Task<LeaveRequest> UpdateLeaveRequest(LeaveRequest leaveRequest);
    }
}
