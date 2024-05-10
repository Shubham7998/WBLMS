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
    [Table("Statuses")]
    [Index(nameof(StatusName), IsUnique = true)]
    public class Status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required(ErrorMessage = "StatusName is required.")]
        public string StatusName { get; set; }
    }
}
