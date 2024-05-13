using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBLMS.Models;

namespace WBLMS.IServices
{
    public interface IEmailService
    {
        void SendEmail(EmailModel emailModel);
    }
}
