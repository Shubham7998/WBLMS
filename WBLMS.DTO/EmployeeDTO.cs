using System.ComponentModel.DataAnnotations;
using WBLMS.Models;

namespace WBLMS.DTO
{
    public record GetEmployeeDTO
        (
            long Id,
            string FirstName,
            string LastName,
            string? ProfilePic,
            string EmailAddress,
            string ContactNumber,
            int GenderId,
            long RoleId,
            long ManagerId,
            long CreatedById,
            DateOnly? JoiningDate
        );

    public record GetEmployeeForeignDTO
        (
            long Id,
            string FirstName,
            string LastName,
            string EmailAddress,
            string ContactNumber,
            int GenderId,
            string? GenderName,
            long RoleId,
            string? RoleName,
            long ManagerId,
            string? ManagerName
        );

    public record GetEmployeesDTO
        (
            long Id,
            string FirstName,
            string LastName,
            string EmailAddress,
            string ContactNumber,
            int GenderId,
            string? GenderName,
            long RoleId,
            string? RoleName,
            string JoiningDate,
            long ManagerId,
            string? ManagerName,
            decimal BalanceLeaveRequest,
            decimal TotalLeaveRequest
        );

    public record GetEmployeeLeaveReqDTO
        (
            long Id,
            string FirstName,
            string LastName,
            string EmailAddress,
            string ContactNumber,
            int GenderId,
            string? GenderName,
            string JoiningDate,
            long RoleId,
            string? RoleName,
            long ManagerId,
            string? ManagerName,
            decimal BalanceLeaveRequest,
            decimal TotalLeaveRequest
        );

    public record CreateEmployeeDTO
        (
            [Required(ErrorMessage = "FirstName is required")]
            [MaxLength(50)] string FirstName,
            [Required(ErrorMessage = "LastName is required")]
            [MaxLength(50)] string LastName,
            [Required(ErrorMessage = "Password is required")]
            [MaxLength(25)] string Password,
            [Required(ErrorMessage = "EmailAddress is required")]
            string EmailAddress,
            [Required(ErrorMessage = "ContactNumber is required")]
            string ContactNumber,
            [Required(ErrorMessage = "Gender is required")]
            int GenderId,
            long RoleId,
            long? ManagerId,
            long? CreatedById
        );

    public record UpdateEmployeeDTO
        (
            long Id,
            [Required(ErrorMessage = "FirstName is required")]
            [MaxLength(50)] string FirstName,
            [Required(ErrorMessage = "LastName is required")]
            [MaxLength(50)] string LastName,
            [Required(ErrorMessage = "Password is required")]
            [MaxLength(25)] string Password,
            [Required(ErrorMessage = "EmailAddress is required")]
            string EmailAddress,
            [Required(ErrorMessage = "ContactNumber is required")]
            string ContactNumber,
            [Required(ErrorMessage = "Gender is required")]
            int GenderId,
            long RoleId,
            long ManagerId,
            long UpdatedById
        );

    public record GetManagerDTO
        (
            long Id, 
            string ManagerName
        );

}
