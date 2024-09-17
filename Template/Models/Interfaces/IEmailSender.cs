using System.Threading.Tasks;

namespace Template.Models.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
