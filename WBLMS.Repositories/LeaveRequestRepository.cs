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

        public async Task<IEnumerable<LeaveRequest>> GetAllLeaveRequests()
        {
            var listOfLeaveRequests = await _dbContext.LeaveRequests
                .Include(employee => employee.Employee)
                .Include(status => status.Status)
                .Include(leaveType => leaveType.LeaveType)
                .ToListAsync();
            return listOfLeaveRequests;
        }
    }
}
