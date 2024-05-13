using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBLMS.Data;
using WBLMS.Data.Migrations;
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

        public async Task<LeaveRequest> CreateLeaveRequest(LeaveRequest leaveRequest)
        {
            var res = await _dbContext.LeaveRequests.AddAsync(leaveRequest);
            var res2 = await _dbContext.SaveChangesAsync();
            var leaveDataFromDb = await _dbContext.LeaveRequests
                .Include(a => a.Employee)
                .Include(b => b.Status)
                .Include(c => c.LeaveType)
                .FirstAsync(leavereq => leavereq.Id == res.Entity.Id);
                   
            return leaveDataFromDb;

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

        public async Task<LeaveRequest> UpdateLeaveRequest(LeaveRequest leaveRequest)
        {
            var request = _dbContext.LeaveRequests.Update(leaveRequest);
            await _dbContext.SaveChangesAsync();
            var leaveDataFromDb = await _dbContext.LeaveRequests
                .Include(a => a.Employee)
                .Include(b => b.Status)
                .Include(c => c.LeaveType)
                .FirstAsync(leavereq => leavereq.Id == request.Entity.Id);

            return leaveDataFromDb;
        }
    }
}
