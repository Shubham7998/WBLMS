using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WBLMS.Models
{
    [Table("Employees")]
    [Index(nameof(EmailAddress), nameof(ContactNumber), IsUnique=true)]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public long Id { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "EmailAddress is required")]
        public string EmailAddress { get; set; }
        
        [Required(ErrorMessage = "Contact Number is required")]
        public string ContactNumber { get; set; }

       // [Required (ErrorMessage ="Gender is required")]
        [ForeignKey(nameof(Gender))]
        public long? GenderId { get; set; }
        public virtual Gender Gender { get; set; }

        [ForeignKey(nameof(Roles))]
        //[Required(ErrorMessage = "Role is required")]
        public long? RoleId { get; set; }
        public virtual Roles Roles { get; set; }

        //[ForeignKey(nameof(Manager))]
        public long? ManagerId { get; set; }
        public Employee? Manager { get; set; }

       // [Required (ErrorMessage ="Created by whom is required")]
        public long? CreatedById { get; set; }

       // [Required(ErrorMessage = "Joining Date is required")]
        public DateOnly? JoiningDate { get; set; }

        //[Required (ErrorMessage = "Update by whom is required")]
        public long? UpdateById { get; set; }

        //[Required (ErrorMessage = "Update date is required")]
        public DateOnly? UpdatedDate { get; set; }

       // [Required (ErrorMessage = "Token Id is required")]
        [ForeignKey (nameof(Token))]
        public long? TokenId { get; set; }
        public virtual Token? Token { get; set; }
        [ForeignKey(nameof(LeaveBalance))]
        public long? LeaveBalanceId { get; set; }
        public virtual LeaveBalance? LeaveBalance { get; set; }
        public List<Employee>? Subordinates { get; set; }
    }

}
