using EcommerceNet8.Core.Aplication.Contracts.Infrastructure;
using EcommerceNet8.Core.Aplication.Models.Email;
using FluentEmail.Core;
using Microsoft.Extensions.Options;

namespace EcommerceNet8.Infraestructure.MessageImplementation
{
    public class EmailService : IEmailService
    {

        private readonly IFluentEmail _fluentEmail;
        private readonly EmailFluentSettings _emailFluentSettings;

        public EmailService(IFluentEmail fluentEmail, IOptions<EmailFluentSettings> emailFluentSettings)
        {
            _fluentEmail = fluentEmail;
            _emailFluentSettings = emailFluentSettings.Value;
        }

        public async Task<bool> SendEmailAsync(EmailMessage email, string token)
        {
            var htmlContent = $"{email.Body} {_emailFluentSettings.BaseUrlClient}/passwordd/reset/{token}";

           var result =await  _fluentEmail
                .To(email.To)
                .Subject(email.Subjet)
                .Body(htmlContent)
                .SendAsync();


            return result.Successful;
        }
    }
}
