
using BestPracticesDemo.Application.Models.Mail;

namespace BestPracticesDemo.Application.Contracts.Persistence
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
