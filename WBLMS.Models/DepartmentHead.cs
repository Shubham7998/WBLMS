using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WBLMS.Models
{
    [Table("DepartmentHeads")]
    [Index(nameof(ContactNumber), IsUnique = true)]
    public class DepartmentHead
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        public DateOnly DOB { get; set; }

        [Required]
        [ForeignKey(nameof(Gender))]
        public int GenderId { get; set; }
        public Gender Gender { get; set; }

        [Required]
        [ForeignKey(nameof(Reporting))]
        public int ReportingId { get; set; }
        public Reporting Reporting { get; set; }

    }
}
