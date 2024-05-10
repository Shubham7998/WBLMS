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
    [Table("Tokens")]
    [Index (nameof(EmployeeId), IsUnique = true)]
    public class Token
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required (ErrorMessage ="Employee id is required")]
        public long EmployeeId { get; set; }
        
        [Required (ErrorMessage = "AccessToken is required")]
        public string AccessToken { get; set; }

        [Required (ErrorMessage = "RefreshToken is required")]
        public string RefreshToken { get; set; }
        
        [Required (ErrorMessage = "RefreshTokenExpiry is required")]
        public DateOnly RefreshTokenExpiry { get; set; }

        [Required (ErrorMessage = "PasswordResetExpiry Time is required")]
        public DateOnly PasswordResetExpiry { get; set; }
        
        [Required (ErrorMessage = "PasswordResetToken is required")]
        public string PasswordResetToken { get; set; }
        
        
    }
}
