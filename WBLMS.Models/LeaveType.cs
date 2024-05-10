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
    [Table("LeaveTypes")]
    [Index(nameof(LeaveTypeName), IsUnique = true)]
    public class LeaveType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required(ErrorMessage = "LeaveTypeName is required.")]
        public string LeaveTypeName { get; set; }
    }
}
