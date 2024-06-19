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
    [Table("Reportings")]
    [Index(nameof(ReportFrom), nameof(ReportTo), IsUnique = true)]
    public class Reporting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        public int ReportTo { get; set; }
        public int ReportFrom { get; set; }
    }
}
