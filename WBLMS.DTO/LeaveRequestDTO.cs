using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBLMS.DTO
{
    public record CreateLeaveRequestDTO(
            [Required(ErrorMessage = "EmployeeId is required.")] long EmployeeId,
            [Required(ErrorMessage = "ManagerId is required.")] long ManagerId,
            [Required(ErrorMessage = "LeaveTypeId is required.")] long LeaveTypeId,
            [Required(ErrorMessage = "Reason is required.")][MaxLength(150, ErrorMessage = "Length cannot exceed 150")] string Reason,
            [Required(ErrorMessage = "StartDate is required.")] DateOnly StartDate,
            [Required(ErrorMessage = "EndDate is required.")] DateOnly EndDate,
            [Required(ErrorMessage = "NumberOfLeaveDays is required.")] decimal NumberOfLeaveDays,
            bool isHalfDay
        );
    public record UpdateLeaveRequestDTO(
            [Required(ErrorMessage = "LeaveRequestId is required.")] long Id,
            long StatusId
        );
    public record GetLeaveRequestDTO(
            long Id,
            long EmployeeId,    
            string FirstName,
            string LastName,
            string LeaveType,
            string Reason,
            string Status,
            DateOnly StartDate,
            DateOnly EndDate,
            decimal NumberOfLeaveDays,
            DateOnly RequestDate,
            DateOnly ApprovedDate
        );

    public record GetLeavesBalanceDTO(
            long Id,
            long EmployeeId,
            decimal Balance,
            decimal TotalLeaves
        );
}
