using Microsoft.Data.SqlClient;
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

        public async Task<(IEnumerable<LeaveRequest>, int)> GetAllLeaveRequests(string? sortColumn, string? sortOrder, int page, int pageSize, LeaveRequest leaveRequestObj)
        {
            //var listOfLeaveRequests = await _dbContext.LeaveRequests
            //    .Include(employee => employee.Employee)
            //    .Include(status => status.Status)
            //    .Include(leaveType => leaveType.LeaveType)
            //    .ToListAsync();

            var query = _dbContext.LeaveRequests
                .Include(e => e.Employee)
                .Include(e => e.Status)
                .Include(e => e.LeaveType)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(leaveRequestObj.Reason))
            {
                query = query.Where(reason => reason.Reason.Contains(leaveRequestObj.Reason));
            }
            if (leaveRequestObj.EmployeeId > 0)
            {
                query = query.Where(leaveRequest => leaveRequest.EmployeeId == leaveRequestObj.EmployeeId);
            }
            if (leaveRequestObj.StatusId > 0)
            {
                query = query.Where(leaveRequest => leaveRequest.StatusId == leaveRequestObj.StatusId);
            }
            if (leaveRequestObj.LeaveTypeId > 0)
            {
                query = query.Where(leaveRequest => leaveRequest.LeaveTypeId == leaveRequestObj.LeaveTypeId);
            }
            if (leaveRequestObj.ManagerId > 0)
            {
                query = query.Where(leaveRequest => leaveRequest.ManagerId == leaveRequestObj.ManagerId);
            }
            if (leaveRequestObj.NumberOfLeaveDays > 0)
            {
                query = query.Where(leaveRequest => leaveRequest.NumberOfLeaveDays  == leaveRequestObj.NumberOfLeaveDays);
            }
            if (!string.IsNullOrEmpty(leaveRequestObj.StartDate.ToString()) && leaveRequestObj.StartDate != DateOnly.MinValue)
            {
                query = query.Where(leaveRequest => leaveRequest.StartDate == leaveRequestObj.StartDate);
            }
            if (!string.IsNullOrEmpty(leaveRequestObj.EndDate.ToString()) && leaveRequestObj.EndDate != DateOnly.MinValue)
            {
                query = query.Where(leaveRequest => leaveRequest.EndDate == leaveRequestObj.EndDate);
            }
            if (!string.IsNullOrEmpty(leaveRequestObj.ApprovedDate.ToString()) && leaveRequestObj.ApprovedDate != DateOnly.MinValue)
            {
                query = query.Where(leaveRequest => leaveRequest.ApprovedDate == leaveRequest.ApprovedDate);
            }
            if (!string.IsNullOrEmpty(leaveRequestObj.RequestDate.ToString()) && leaveRequestObj.RequestDate != DateOnly.MinValue)
            {
                query = query.Where(leaveRequest => leaveRequest.RequestDate == leaveRequestObj.RequestDate);
            }

            // Sorting 

            if (!string.IsNullOrWhiteSpace(sortColumn) && !string.IsNullOrWhiteSpace(sortOrder))
            {
                // Determine the sort order based on sortOrder parameter
                bool isAscending = sortOrder.ToLower() == "asc";
                switch (sortColumn.ToLower())
                {
                    case "firstname":
                        query = isAscending ? query.OrderBy(s => s.Employee.FirstName) : query.OrderByDescending(s => s.Employee.FirstName);
                        break;
                    case "lastname":
                        query = isAscending ? query.OrderBy(s => s.Employee.LastName) : query.OrderByDescending(s => s.Employee.LastName);
                        break;
                    case "reason":
                        query = isAscending ? query.OrderBy(s => s.Reason) : query.OrderByDescending(s => s.Reason);
                        break;
                    case "leaveTypeName":
                        query = isAscending ? query.OrderBy(s => s.LeaveType.LeaveTypeName) : query.OrderByDescending(s => s.LeaveType.LeaveTypeName);
                        break;
                    case "statusName":
                        query = isAscending ? query.OrderBy(s => s.Status.StatusName) : query.OrderByDescending(s => s.Status.StatusName);
                        break;
                    case "manager":
                        query = isAscending ? query.OrderBy(s => s.Manager.FirstName) : query.OrderByDescending(s => s.Manager.FirstName);
                        break;
                    case "startdate":
                        query = isAscending ? query.OrderBy(s => s.StartDate) : query.OrderByDescending(s => s.StartDate);
                        break;
                    case "enddate":
                        query = isAscending ? query.OrderBy(s => s.EndDate) : query.OrderByDescending(s => s.EndDate);
                        break;
                    case "numberofleavedays":
                        query = isAscending ? query.OrderBy(s => s.NumberOfLeaveDays) : query.OrderByDescending(s => s.NumberOfLeaveDays);
                        break;
                    case "approveddate":
                        query = isAscending ? query.OrderBy(s => s.ApprovedDate) : query.OrderByDescending(s => s.ApprovedDate);
                        break;
                    case "requestdate":
                        query = isAscending ? query.OrderBy(s => s.RequestDate) : query.OrderByDescending(s => s.RequestDate);
                        break;
                    default:
                        query = query.OrderBy(s => s.Id);
                        break;
                }

            }

            int totalCount = query.Count();
            int totalPages = (int)(Math.Ceiling((decimal)totalCount / pageSize));

            query = query.Skip((page - 1) * pageSize).Take(pageSize);
            return (await query.ToListAsync(), totalPages);
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
