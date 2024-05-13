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
        public long LeaveTypeId { get; set; }
        [Required(ErrorMessage = "Reason is required.")]
        public string Reason { get; set; }
        [Required(ErrorMessage = "StatusId is required.")]
        [ForeignKey(nameof(Status))]
        public long StatusId { get; set; }
        [Required(ErrorMessage = "ManagerId is required.")]
        [ForeignKey(nameof(Manager))]
        public long ManagerId { get; set; }
        [Required(ErrorMessage = "StartDate is required.")]
        public DateOnly StartDate { get; set; } = DateOnly.MinValue;
        [Required(ErrorMessage = "EndDate is required.")]
        public DateOnly EndDate { get; set; } = DateOnly.MinValue;
        [Required(ErrorMessage = "NumberOfLeaveDays is required.")]
        public decimal NumberOfLeaveDays { get; set; }
        [Required(ErrorMessage = "ApprovedDate is required.")]
        public DateOnly ApprovedDate { get; set; }
        [Required(ErrorMessage = "RequestDate is required.")]
        public DateOnly RequestDate { get; set; }


        public virtual LeaveType LeaveType { get; set; }
        public virtual Status Status { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Employee Manager { get; set; }
    }
}
