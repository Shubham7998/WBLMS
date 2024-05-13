using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBLMS.Utilities
{
    public class EmailSettings
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public long Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
