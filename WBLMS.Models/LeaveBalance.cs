using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBLMS.Models
{
    [Table("LeaveBalances")]
    [Index(nameof(EmployeeId), IsUnique = true)]
    public class LeaveBalance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required(ErrorMessage = "EmployeeId is required.")]
        [ForeignKey(nameof(Employee))]
        public long EmployeeId { get; set; }
        [Required(ErrorMessage = "Balance is required.")]
        public decimal Balance { get; set; }
        [Required(ErrorMessage = "TotalLeaves is required.")]
        public decimal TotalLeaves { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
