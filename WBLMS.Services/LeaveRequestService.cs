﻿using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using WBLMS.Data;
using WBLMS.DTO;
using WBLMS.IRepositories;
using WBLMS.IServices;
using WBLMS.Models;
using WBLMS.Utilities;
namespace WBLMS.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly WBLMSDbContext _dbContext;
        private readonly IEmailService _emailService;

        public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository, WBLMSDbContext dbContext, IEmailService emailService)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _dbContext = dbContext;
            _emailService = emailService;
        }

        public async Task<GetLeaveRequestDTO> CreateLeaveRequest(CreateLeaveRequestDTO leaveRequestDTO)
        {
            if (leaveRequestDTO != null)
            {
                // Get Manager From the table using Employee Id to get manager Id
                // Calculate Number of leave days

                var employee = await _dbContext.Employees.FindAsync(leaveRequestDTO.EmployeeId);
                var newLeaveRequestObj = new LeaveRequest()
                {
                    EmployeeId = leaveRequestDTO.EmployeeId,
                    LeaveTypeId = leaveRequestDTO.LeaveTypeId,
                    Reason = leaveRequestDTO.Reason,
                    // Default is Pending so 1
                    StatusId = 1,
                    ManagerId = (long)employee.ManagerId,
                    StartDate = leaveRequestDTO.StartDate,
                    EndDate = leaveRequestDTO.EndDate,
                    // Calculate Number of working days from start date and end date
                    NumberOfLeaveDays = GetBuisnessDays(leaveRequestDTO.StartDate, leaveRequestDTO.EndDate, leaveRequestDTO.isHalfDay),
                    // ApprovedDate 

                    // RequestDate is set to current Date
                    RequestDate = DateOnly.FromDateTime(DateTime.UtcNow.Date)
                };
                var returnObj = await _leaveRequestRepository.CreateLeaveRequest(newLeaveRequestObj);
                if (returnObj != null)
                {
                    var returnObjDTO = new GetLeaveRequestDTO(
                        returnObj.Id, returnObj.EmployeeId, returnObj.ManagerId, returnObj.Employee.FirstName, returnObj.Employee.LastName,
                        returnObj.LeaveType.LeaveTypeName, returnObj.Reason, returnObj.Status.StatusName, returnObj.StartDate,
                        returnObj.EndDate, returnObj.NumberOfLeaveDays, returnObj.RequestDate, returnObj.ApprovedDate, returnObj.Employee.Roles.RoleName
                    );
                    // Send Email

                    var manager = await _dbContext.Employees.FindAsync(employee.ManagerId);
                    var managerName = manager.FirstName + " " + manager.LastName;
                    var emailModel = new EmailModel(manager.EmailAddress, "Leave Request ", EmailBody.LeaveRequestEmailBody(manager.EmailAddress, managerName, returnObjDTO));

                    _emailService.SendEmail(emailModel);

                    //_dbContext.Entry(leaveRequestDTO).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();

                    return returnObjDTO;
                }
                return null;

            }
            return null;
        }

        public async Task<(IEnumerable<GetLeaveRequestDTO>, int)> GetAllLeaveRequests(string? sortColumn, string? sortOrder, int page, int pagesize, GetLeaveRequestDTO leaveRequestObj, string? searchKeyword)
        {
            var listOfLeaveRequestsTuple = await _leaveRequestRepository.GetAllLeaveRequests(sortColumn, sortOrder, page, pagesize, leaveRequestObj, searchKeyword);

            if (listOfLeaveRequestsTuple.Item1 != null)
            {
                var listOfLeaveRequestDTO = listOfLeaveRequestsTuple
                    .Item1
                    .Select(request => new GetLeaveRequestDTO(
                        request.Id, request.EmployeeId, request.ManagerId, request.Employee.FirstName, request.Employee.LastName, request.LeaveType.LeaveTypeName, request.Reason, request.Status.StatusName, request.StartDate, request.EndDate, request.NumberOfLeaveDays, request.RequestDate, request.ApprovedDate, request.Employee.Roles.RoleName
                    )
                );
                return (listOfLeaveRequestDTO, listOfLeaveRequestsTuple.Item2);
            }
            return (null, 0);
        }

        public async Task<GetLeaveRequestDTO> GetLeaveRequestById(long id)
        {
            var leaveRequest = await _leaveRequestRepository.GetLeaveRequestById(id);
            if (leaveRequest != null)
            {
                var leaveRequestDTO = new GetLeaveRequestDTO(
                        leaveRequest.Id, leaveRequest.EmployeeId, leaveRequest.ManagerId, leaveRequest.Employee.FirstName, leaveRequest.Employee.LastName,
                        leaveRequest.LeaveType.LeaveTypeName, leaveRequest.Reason, leaveRequest.Status.StatusName, leaveRequest.StartDate,
                        leaveRequest.EndDate, leaveRequest.NumberOfLeaveDays, leaveRequest.RequestDate, leaveRequest.ApprovedDate, leaveRequest.Employee.Roles.RoleName
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
                {
                    if (isHalfDay)
                    {
                        return 0.5M;
                    }
                    return 1;

                }
                return 0;
            }

            while (StartDate <= EndDate)
            {
                if (StartDate.DayOfWeek != DayOfWeek.Saturday && StartDate.DayOfWeek != DayOfWeek.Sunday)
                    ++counter;
                StartDate = StartDate.AddDays(1);
            }

            return counter;
        }

        public async Task<GetLeaveRequestDTO> UpdateLeaveRequest(UpdateLeaveRequestDTO leaveRequestDTO)
        {
            var oldLeaveRequest = await _leaveRequestRepository.GetLeaveRequestById(leaveRequestDTO.Id);

            if (oldLeaveRequest != null)
            {
                // Updating status 
                oldLeaveRequest.StatusId = leaveRequestDTO.StatusId;

                // If Status is Approved then we Update Balance on the LeaveBalances Table
                // Need to implement that

                // Storing the Updated Obj
                var returnObj = await _leaveRequestRepository.UpdateLeaveRequest(oldLeaveRequest);

                var returnObjDTO = new GetLeaveRequestDTO(
                        returnObj.Id, returnObj.EmployeeId, returnObj.ManagerId, returnObj.Employee.FirstName, returnObj.Employee.LastName,
                        returnObj.LeaveType.LeaveTypeName, returnObj.Reason, returnObj.Status.StatusName, returnObj.StartDate,
                        returnObj.EndDate, returnObj.NumberOfLeaveDays, returnObj.RequestDate, returnObj.ApprovedDate, returnObj.Employee.Roles.RoleName
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

        public async Task<GetLeavesBalanceDTO> GetLeavesBalanceById(long employeeId)
        {
            var leavesBalance = await _leaveRequestRepository.GetLeavesBalanceById(employeeId);
            var leavesBalanceDTO = new GetLeavesBalanceDTO(
                    leavesBalance.Id, leavesBalance.EmployeeId, leavesBalance.Balance, leavesBalance.TotalLeaves
                );
            return leavesBalanceDTO;
        }

        public async Task<IEnumerable<GetLeaveTypesDTO>> GetLeavesType()
        {
            try
            {
                var leaveTypes = await _dbContext.LeaveTypes.ToListAsync();



                var leaveTypeDTO = leaveTypes.Select
                (
                    leaveType => new GetLeaveTypesDTO(
                        leaveType.Id,
                        leaveType.LeaveTypeName
                        )
                );

                return leaveTypeDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<GetLeaveStatusesDTO>> GetLeavesStatuses()
        {
            try
            {
                var leaveStatuses = await _dbContext.Statuses.ToListAsync();

                var leaveStatusesDTO = leaveStatuses.Select
                (
                    leaveStatus => new GetLeaveStatusesDTO(
                        leaveStatus.Id,
                        leaveStatus.StatusName
                        )
                );

                return leaveStatusesDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<(IEnumerable<GetLeaveRequestDTO>, int)> SearchLeaveRequests(int page, int pageSize, string? search, long employeeId, long managerId)
        {
            try
            {
                var listOfLeaveRequestsTuple = await _leaveRequestRepository.SearchLeaveRequests(page, pageSize, search, employeeId, managerId);

                if (listOfLeaveRequestsTuple.Item1 != null)
                {
                    var listOfLeaveRequestDTO = listOfLeaveRequestsTuple
                        .Item1
                        .Select(request => new GetLeaveRequestDTO(
                            request.Id, request.EmployeeId, request.ManagerId, request.Employee.FirstName, request.Employee.LastName, request.LeaveType.LeaveTypeName, request.Reason, request.Status.StatusName, request.StartDate, request.EndDate, request.NumberOfLeaveDays, request.RequestDate, request.ApprovedDate, request.Employee.Roles.RoleName
                        )
                    );
                    return (listOfLeaveRequestDTO, listOfLeaveRequestsTuple.Item2);
                }
                return (null, 0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetCountOfLeaveStatusesDTO> GetCountOfLeaveStatuses(long employeeId)
        {
            var result = await _leaveRequestRepository.GetCountOfLeaveStatuses(employeeId);
            return result;
        }
        public async Task<IEnumerable<GetWonderbizLeaveDTO>> GetWonderbizHolidays()
        {
            try
            {
                var wonderBizHolidays = await _dbContext.WonderbizHolidays.ToListAsync();

                var holidayListDTO = wonderBizHolidays.Select
                    (
                        wbHoliday =>
                        new GetWonderbizLeaveDTO
                        (
                                wbHoliday.Id,
                                wbHoliday.Date,
                                wbHoliday.Day,
                                wbHoliday.Event
                        )
                    );

                return holidayListDTO;
            }catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetLeaveRequestByYear> GetLeaveRequestCountByYear(long year, long employeeId)
        {
            //var leaveRequests = await _dbContext.LeaveRequests.ToListAsync();

            var leaveReqDTO = new GetLeaveRequestByYear(
                        await GetLeaveRequestCountForMonth(1, year, employeeId),
                        await GetLeaveRequestCountForMonth(2, year, employeeId),
                        await GetLeaveRequestCountForMonth(3, year, employeeId),
                        await GetLeaveRequestCountForMonth(4, year, employeeId),
                        await GetLeaveRequestCountForMonth(5, year, employeeId),
                        await GetLeaveRequestCountForMonth(6, year, employeeId),
                        await GetLeaveRequestCountForMonth(7, year, employeeId),
                        await GetLeaveRequestCountForMonth(8, year, employeeId),
                        await GetLeaveRequestCountForMonth(9, year, employeeId),
                        await GetLeaveRequestCountForMonth(10, year, employeeId),
                        await GetLeaveRequestCountForMonth(11, year, employeeId),
                        await GetLeaveRequestCountForMonth(12, year, employeeId)
                    );
            return leaveReqDTO;
        }

        public  async Task<LeaveRequestStatusDTO> GetLeaveRequestCountForMonth(int month, long year, long employeeId)
        {
            var leaveReqForMonth = await  _dbContext.LeaveRequests
                .Where(lr => lr.StartDate.Month == month && lr.StartDate.Year == year)
                .ToListAsync();

            if (employeeId != 0)
            {
                leaveReqForMonth = leaveReqForMonth.Where(emp => emp.EmployeeId == employeeId).ToList();
            }

            var appliedLeaveRequests = leaveReqForMonth.Count;
            var acceptedLeaveRequests = leaveReqForMonth.Count(lr => lr.StatusId == 2); 
            var rejectedLeaveRequests = leaveReqForMonth.Count(lr => lr.StatusId == 3); 
            var pendingLeaveRequests = leaveReqForMonth.Count(lr => lr.StatusId == 1); 

            var dto =  new LeaveRequestStatusDTO(
                appliedLeaveRequests,
                acceptedLeaveRequests,
                rejectedLeaveRequests,
                pendingLeaveRequests
            );

            return dto;
        }

    }
}
