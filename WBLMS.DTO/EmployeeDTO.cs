using System.ComponentModel.DataAnnotations;
using WBLMS.Models;

namespace WBLMS.DTO
{
    public record GetEmployeeDTO
        (
            long Id,
            string FirstName,
            string LastName,
            string? Password,
            string EmailAddress,
            string ContactNumber,
            long GenderId,
            string? GenderName,
            long RoleId,
            string? RoleName,
            long ManagerId,
            string? ManagerName,
            long CreatedById,
            string? CreatedByName,
            DateOnly? UpdatedDate,
            List<Employee>? Subordinates
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
            long GenderId,
            long RoleId,
            long ManagerId,
            long CreatedById
        );

    public record UpdateEmployeeDTO
        (
            long Id,
            [Required(ErrorMessage = "FirstName is required")]
            [MaxLength(50)] string FirstName,
            [Required(ErrorMessage = "LastName is required")]
            [MaxLength(50)] string LastName,
            [Required(ErrorMessage = "EmailAddress is required")]
            string EmailAddress,
            [Required(ErrorMessage = "ContactNumber is required")]
            string ContactNumber,
            [Required(ErrorMessage = "Gender is required")]
            long GenderId,
            long RoleId,
            long ManagerId,
            long UpdatedById
        );

}
