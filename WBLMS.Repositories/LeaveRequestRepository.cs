using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBLMS.Data;
using WBLMS.IRepositories;
using WBLMS.Models;

namespace WBLMS.Repositories
{
    public class LeaveRequestRepository : Repository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly WBLMSDbContext _dbContext;
        public LeaveRequestRepository(WBLMSDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LeaveRequest> CreateLeaveRequest(LeaveRequest leaveRequestObj)
        {
            await _dbContext.AddAsync(leaveRequestObj);

            return leaveRequestObj;
        }

        public async Task<IEnumerable<LeaveRequest>> GetAllLeaveRequests()
        {
            var listOfLeaveRequests = await _dbContext.LeaveRequests
                .Include(employee => employee.Employee)
                .Include(status => status.Status)
                .Include(leaveType => leaveType.LeaveType)
                .ToListAsync();
            return listOfLeaveRequests;
        }

        public async Task<LeaveRequest> GetLeaveRequestById(long id)
        {
            var request = await _dbContext.LeaveRequests
                .Include(emp => emp.Employee)
                .Include(sta => sta.Status)
                .Include(typ => typ.LeaveType)
                .FirstAsync(leavereq => leavereq.Id == id);
            return request;
        }
    }
}
