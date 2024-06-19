using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBLMS.Models
{
    [Table("Teams")]
    [Index(nameof(Name), IsUnique = true)]
    public class Team
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }

        public Department Department { get; set; }

        [ForeignKey(nameof(Employee2))]
        public int? TeamLeader { get; set; }
        public Employee2 Employee2 { get; set; }

        public ICollection<Employee2> TeamMembers { get; set; }
    }
}
