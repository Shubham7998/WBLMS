using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBLMS.Models
{
    [Table("SuperAdmins")]
    public class SuperAdmin : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "EmailAddress is required")]
        public string EmailAddress { get; set; }
        [Required]
        public string ContactNumber { get; set; }

        public string? ProfilePicture { get; set; }

        [Required]
        [ForeignKey(nameof(Gender))]
        public int GenderId { get; set; }
        public Gender Gender { get; set; }

        [ForeignKey(nameof(Token))]
        public long? TokenId { get; set; }
        public virtual Token? Token { get; set; }
    }
}
