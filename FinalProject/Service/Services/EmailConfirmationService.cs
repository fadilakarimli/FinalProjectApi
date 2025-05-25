using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Service.Helpers;
using Service.Services.Interfaces;
using MailKit.Net.Smtp;

namespace Service.Services
{
    public class EmailConfirmationService  : IEmailConfirmationService
    {
        private readonly EmailSettings _emailSettings;

        public EmailConfirmationService(IOptions<EmailSettings> emailsettings)
        {
            _emailSettings = emailsettings.Value;
        }

        public void Send(string to, string subject, string html, string from = null)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from ?? _emailSettings.FromAddress));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            using var smtp = new SmtpClient();
            smtp.Connect(_emailSettings.Server, _emailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.UserName, _emailSettings.Password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
