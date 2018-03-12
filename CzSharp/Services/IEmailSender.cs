using System.Threading.Tasks;

namespace CzSharp.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string content);
    }
}