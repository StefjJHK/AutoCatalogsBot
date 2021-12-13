using BusinessLogic.Services.Requests;
using Microsoft.Extensions.Logging;
using Presentation.Aspects.Utility;
using System;

namespace Presentation.Aspects.Logging
{
    public class RequestServiceLoggingDecorator<T, K> : IRequestService<T, K> where T : class
    {
        private readonly IRequestService<T, K> _decorate;
        private readonly ILogger<RequestServiceLoggingDecorator<T, K>> _logger;

        public RequestServiceLoggingDecorator(IRequestService<T, K> decorate, ILogger<RequestServiceLoggingDecorator<T, K>> logger)
        {
            _decorate = decorate ??
                throw new ArgumentNullException(nameof(decorate));
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        public K Execute(T request)
        {
            _logger.LogInformation("[{DateTime}] Handling {RequestName}; {Metadata}", 
                DateTimeOffset.Now, typeof(T).Name, LoggingStatics.GetMetadata(request));

            var result = _decorate.Execute(request);

            _logger.LogInformation("[{DateTime}] Completed handling {Request}; {Metadata}; Result: {Result}", 
                DateTimeOffset.Now, typeof(T).Name, LoggingStatics.GetMetadata(request), LoggingStatics.GetMetadata(result));

            return result;
        }
    }
}
