using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace CzSharp.Services
{
    public class EmailSender: IEmailSender
    {
        private string server;

        private MailAddress sender;
        private NetworkCredential credentials;
        
        public EmailSender(IConfiguration configuration)
        {
            server = configuration["Email:Server"];
            sender = new MailAddress(configuration["Email:Email"], configuration["Email:Name"]);
            credentials = new NetworkCredential(configuration["Email:Email"], configuration["Email:Password"]);
        }
        
        public async Task SendEmailAsync(string to, string subject, string content)
        {
            var toAddress = new MailAddress(to);
            var message = new MailMessage
            {
                Subject = subject,
                From = sender,
                To = { toAddress },
                Body = content,
                IsBodyHtml = true
            };

            using (var client = new SmtpClient(server)
            {
                UseDefaultCredentials = false,
                Credentials = credentials
            })
            {
                await client.SendMailAsync(message);
            }
        }
    }
}