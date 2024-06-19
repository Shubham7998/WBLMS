using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBLMS.Models
{
    public class Employee2 : BaseEntity
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
        [Required]
        [ForeignKey(nameof(Gender))]
        public int GenderId { get; set; }
        public Gender Gender { get; set; }

        [Required]
        [ForeignKey(nameof(Reporting))]
        public int ReportingId { get; set; }
        public Reporting Reporting { get; set; }

        public Team Team { get; set; }
        public int? TeamId { get; set; }
    }
}
