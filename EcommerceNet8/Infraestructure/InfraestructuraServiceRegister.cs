using EcommerceNet8.Core.Aplication.Models.Token;
using EcommerceNet8.Core.Aplication.Persistence;
using EcommerceNet8.Infraestructure.Repository;

namespace EcommerceNet8.Infraestructure
{
    public static class InfraestrucutreServiceRegister
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));


            return services;
        }
    }
}
