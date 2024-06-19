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
    [Index(nameof(ContactNumber), IsUnique = true)]
    public class User : BaseEntity
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

    }
}