using EcommerceNet8.Core.Aplication.Contracts.Identity;
using EcommerceNet8.Core.Aplication.Contracts.Infrastructure;
using EcommerceNet8.Core.Aplication.Models.Email;
using EcommerceNet8.Core.Aplication.Models.ImageMangment;
using EcommerceNet8.Core.Aplication.Models.Token;
using EcommerceNet8.Core.Aplication.Persistence;
using EcommerceNet8.Infraestructure.MessageImplementation;
using EcommerceNet8.Infraestructure.Repository;
using EcommerceNet8.Infraestructure.Services.Auth;

namespace EcommerceNet8.Infraestructure
{
    public static class InfraestrucutreServiceRegister
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            


            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));


            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IAuthService, AuthService>();
            services.Configure<EmailFluentSettings>(configuration.GetSection("EmailFluentSettings"));


            return services;
        }
    }
}
