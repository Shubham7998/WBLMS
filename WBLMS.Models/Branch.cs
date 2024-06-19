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
    [Table("Branches")]
    [Index(nameof(Name), IsUnique = true)]
    public class Branch
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int OrganizationId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public Organization Organization { get; set; }
        public ICollection<Department> Departments { get; set; } = new List<Department>();
        public ICollection<LeaveType> LeaveTypes { get; set; } = new List<LeaveType>();
        public WorkingDays WorkingDays { get; set; }
        public ICollection<Roles> Roles { get; set; }
        public ICollection<Holiday> Holidays { get; set; }


    }
}
