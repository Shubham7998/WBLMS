using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBLMS.Utilities
{
    public class JwtSettings
    {
        public string AccessTokenExpirationMinutes {  get; set; }
        public string RefreshTokenExpirationDays {  get; set; }
        public string Key {  get; set; }
        public string Issuer {  get; set; }
        public string Audience {  get; set; }
        public string Subject {  get; set; }
    }
}
