﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBLMS.DTO
{
    public record CreateLeaveRequestDTO(
            [Required(ErrorMessage = "EmployeeId is required.")] long EmployeeId,
            // [Required(ErrorMessage = "ManagerId is required.")] long ManagerId,
            [Required(ErrorMessage = "LeaveTypeId is required.")] long LeaveTypeId,
            [Required(ErrorMessage = "Reason is required.")][MaxLength(150, ErrorMessage = "Length cannot exceed 150")] string Reason,
            [Required(ErrorMessage = "StartDate is required.")] DateOnly StartDate,
            [Required(ErrorMessage = "EndDate is required.")] DateOnly EndDate,
            [Required(ErrorMessage = "NumberOfLeaveDays is required.")] decimal NumberOfLeaveDays,
            bool isHalfDay
        );

    public record GetLeaveRequestByYear(
            LeaveRequestStatusDTO January,
            LeaveRequestStatusDTO February,
            LeaveRequestStatusDTO March,
            LeaveRequestStatusDTO April,
            LeaveRequestStatusDTO May,
            LeaveRequestStatusDTO June,
            LeaveRequestStatusDTO July,
            LeaveRequestStatusDTO August,
            LeaveRequestStatusDTO September,
            LeaveRequestStatusDTO October,
            LeaveRequestStatusDTO November,
            LeaveRequestStatusDTO December
        );

    public record LeaveRequestStatusDTO(
            long AppliedLeaveRequests,
            long AcceptedLeaveRequests,
            long RejectedLeaveRequests,
            long PendingLeaveRequests
        );
    public record UpdateLeaveRequestDTO(
            [Required(ErrorMessage = "LeaveRequestId is required.")] long Id,
            long StatusId
        );
    public record GetLeaveRequestDTO(
            long Id,
            long EmployeeId,
            long? ManagerId,
            string FirstName,
            string LastName,
            string LeaveType,
            string Reason,
            string Status,
            DateOnly? StartDate,
            DateOnly? EndDate,
            decimal NumberOfLeaveDays,
            DateOnly? RequestDate,
            DateOnly? ApprovedDate,
            string? RoleName
        );

    public record GetLeavesBalanceDTO(
            long Id,
            long EmployeeId,
            decimal Balance,
            decimal TotalLeaves
        );

    public record GetCountOfLeaveStatusesDTO(
            decimal approvedLeavesCount,
            decimal pendingLeavesCount,
            decimal rejectedLeavesCount
        );
}
