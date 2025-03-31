using MediatR;
using Org.BouncyCastle.Utilities;

namespace EcommerceNet8.Core.Aplication.Behavior
{
    //Este comportamiento captura excepciones que ocurran en el manejador o en pasos posteriores del pipeline.
    //Te salva de errores inesperados y te da logs para debuggear.
    public class UnhandledExceptionBehavior<TRequest, TResponse>
                                : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogError(ex, "Application Request: Sucedio una exception para el request {Name} {@Request}", requestName, request);
                throw new Exception("Application Request con Errores");
            }

        }
    }
}
