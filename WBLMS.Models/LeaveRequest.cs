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
    [Table("LeaveRequests")]
    public class LeaveRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public long Id { get; set; }
        [Required(ErrorMessage = "EmployeeId is required.")]
        [ForeignKey(nameof(Employee))]
        public long EmployeeId { get; set; }
        [Required(ErrorMessage = "LeaveTypeId is required.")]
        [ForeignKey(nameof(LeaveType))]
        public int LeaveTypeId { get; set; }
        [Required(ErrorMessage = "Reason is required.")]
        public string Reason { get; set; }
        [Required(ErrorMessage = "StatusId is required.")]
        [ForeignKey(nameof(Status))]
        public long StatusId { get; set; }
        [Required(ErrorMessage = "ManagerId is required.")]
        [ForeignKey(nameof(Manager))]
        public long ManagerId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        [Required(ErrorMessage = "NumberOfLeaveDays is required.")]
        public decimal NumberOfLeaveDays { get; set; }
        public DateOnly ApprovedDate { get; set; }
        public DateOnly RequestDate { get; set; }


        public virtual LeaveType LeaveType { get; set; }
        public virtual Status Status { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Employee Manager { get; set; }
    }
}
