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
    [Table("Holidays")]
    [Index(nameof(HolidayDate), IsUnique = true)]
    public class Holiday
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string HolidayName { get; set; }
        public DateOnly HolidayDate { get; set; }
        public Branch Branch { get; set; }
    }
}
