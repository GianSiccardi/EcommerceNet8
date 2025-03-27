using EcommerceNet8.Core.Aplication.Models.Email;

namespace EcommerceNet8.Core.Aplication.Contracts.Infrastructure
{
    public interface IEmailService
    {

        Task<bool> SendEmailAsync(EmailMessage email, string token );
    }
}
