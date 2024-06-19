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
    [Table("Departments")]
    [Index(nameof(Name), IsUnique = true)]
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        public ICollection<Team> Teams { get; set; }

        [ForeignKey(nameof(Employee2))]  
        public int? DepartmentHead { get; set; }
        public virtual Employee2 Employee2{ get; set; }
    }
}
