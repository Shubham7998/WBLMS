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
    [Table("Organizations")]
    [Index(nameof(Name), IsUnique = true)]
    public class Organization
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string HeadQuarter { get; set; }

        public ICollection<Branch> Branches { get; set; } = new List<Branch>();

        [Required]
        [ForeignKey(nameof(SuperAdmin))]
        public int SuperAdminId { get; set; }
        public SuperAdmin SuperAdmin { get; set; }
    }
}
