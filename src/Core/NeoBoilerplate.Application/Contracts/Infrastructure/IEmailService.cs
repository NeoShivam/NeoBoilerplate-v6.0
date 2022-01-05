using NeoBoilerplate.Application.Models.Mail;
using System.Threading.Tasks;

namespace NeoBoilerplate.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
