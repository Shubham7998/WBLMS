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
    [Table("LeaveSubTypes")]
    [Index(nameof(LeaveSubTypeName), IsUnique = true)]
    public class LeaveSubType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Required]
        public int LeaveTypeId { get; set; }
        [Required]
        public string LeaveSubTypeName { get; set; }
        [Required]
        public decimal MaxLeaveDays { get; set; }
        public LeaveType LeaveType { get; set; }
    }
}
