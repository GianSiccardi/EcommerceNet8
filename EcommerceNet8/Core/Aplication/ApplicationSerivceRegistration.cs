using AutoMapper;
using EcommerceNet8.Core.Aplication.Behavior;
using EcommerceNet8.Core.Aplication.Extensions;
using EcommerceNet8.Core.Aplication.Mapping;
using MediatR;
using System.Runtime.CompilerServices;

namespace EcommerceNet8.Core.Aplication
{
    public static class ApplicationSerivceRegistration
    {
        public static IServiceCollection AddApplicationServices(
                     this IServiceCollection services,
                     IConfiguration configuration
 )
        {
            var mapperConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));




            services.AddServiceEmail(configuration);
            return services;
        }
    }
}
