using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBLMS.DTO
{
    public record ResetPasswordDTO
    {
        public string Email { get; set; }
        public string EmailToken { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}
