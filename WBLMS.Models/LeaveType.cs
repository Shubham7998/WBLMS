﻿using Microsoft.EntityFrameworkCore;
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
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "LeaveTypeName is required.")]
        public string LeaveTypeName { get; set; }
        [Required]
        public int BranchId { get; set; }
        [Required]
        public decimal MaxDays { get; set; }

        public Branch Branch { get; set; }
        public ICollection<LeaveSubType> LeaveSubTypes { get; set; }    
    }
}
