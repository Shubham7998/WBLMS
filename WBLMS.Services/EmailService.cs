using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBLMS.IServices;
using WBLMS.Models;
using MailKit.Net.Smtp;
using WBLMS.Utilities;
using MailKit.Net.Smtp;
using MimeKit;

namespace WBLMS.Services
{
    public class EmailService : IEmailService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<JwtSettings> jwtSettings, IOptions<EmailSettings> emailSettings)
        {
            _jwtSettings = jwtSettings.Value;
            _emailSettings = emailSettings.Value;
        }

        public void SendEmail(EmailModel emailModel)
        {
            var emailMessage = new MimeMessage();
            var from = _emailSettings.From;
            emailMessage.From.Add(new MailboxAddress("JWT auth", from));
            emailMessage.To.Add(new MailboxAddress(emailModel.To, emailModel.To));
            emailMessage.Subject = emailMessage.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = string.Format(emailModel.Content),
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailSettings.SmtpServer, 465, true);
                    client.Authenticate(_emailSettings.From, _emailSettings.Password);
                    client.Send(emailMessage);
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
