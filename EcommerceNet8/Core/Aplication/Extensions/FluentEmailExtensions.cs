using EcommerceNet8.Core.Aplication.Models.Email;

namespace EcommerceNet8.Core.Aplication.Extensions
{
    public static class FluentEmailExtensions
    {

        public static void AddServiceEmail(this IServiceCollection services , IConfiguration configutation)
        {
            services.Configure<EmailFluentSettings>(configutation.GetSection(nameof(EmailFluentSettings)));

            var emailSettings = configutation.GetSection(nameof(EmailFluentSettings));
            var fromEmail = emailSettings.GetValue<string>("Email");
            var host = emailSettings.GetValue<string>("Host");
            var port = emailSettings.GetValue<int>("Port");

            services.AddFluentEmail(fromEmail).AddSmtpSender(host, port);

        }
    }
}
