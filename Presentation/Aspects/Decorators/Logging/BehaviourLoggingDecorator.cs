using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Presentation.Aspects.Implementation.CircutBraker;
using Presentation.Aspects.Utility;

namespace Presentation.Aspects.Logging
{
    public class BehaviourLoggingDecorator<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<BehaviourLoggingDecorator<TRequest, TResponse>> _logger;

        public BehaviourLoggingDecorator(ILogger<BehaviourLoggingDecorator<TRequest, TResponse>> logger)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation("[{DateTime}] Handling {RequestName}; {RequestMetada}",
                DateTimeOffset.Now, typeof(TRequest).Name, LoggingStatics.GetMetadata(request));

            try
            {
                var response = await next();

                _logger.LogInformation("[{DateTime}] Handled {RequestName}",
                    DateTimeOffset.Now, typeof(TRequest).Name);

                return response;
            }
            catch (Exception exception) when (
                exception is CommandCircutBreakerException ||
                exception is RequestCircutBreakerException)
            {
                _logger.LogWarning("[{DateTime}] throwed [{ExceptionName}]; {ExceptionMetadata}",
                    DateTimeOffset.Now, exception.GetType().Name, LoggingStatics.GetMetadata(exception));
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError("[{DateTime}] throwed [{ExceptionName}]; {ExceptionMetadata}",
                    DateTimeOffset.Now, exception.GetType().Name, LoggingStatics.GetMetadata(exception));
                throw;
            }
        }
    }
}
