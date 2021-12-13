using MediatR;
using Microsoft.Extensions.Logging;
using Presentation.Aspects.Utility;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.Aspects.Decorators.Logging
{
    public class NotificationLoggingDecorator<T> : INotificationHandler<T> 
        where T : INotification
    {
        private readonly ILogger<NotificationLoggingDecorator<T>> _logger;
        private readonly INotificationHandler<T> _decorate;

        public NotificationLoggingDecorator(ILogger<NotificationLoggingDecorator<T>> logger, 
            INotificationHandler<T> decorate)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _decorate = decorate ??
                throw new ArgumentNullException(nameof(decorate));
        }

        public Task Handle(T notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[{DateTime}] Handling {RequestName}; {RequestMetada}",
               DateTimeOffset.Now, typeof(T).Name, LoggingStatics.GetMetadata(notification));

            try
            {
                _decorate.Handle(notification, cancellationToken);

                _logger.LogInformation("[{DateTime}] Handled {RequestName}",
                    DateTimeOffset.Now, typeof(T).Name);
            }
            catch (Exception exception)
            {
                _logger.LogWarning("[{DateTime}] throwed [{ExceptionName}]; {ExceptionMetadata}",
                    DateTimeOffset.Now, exception.GetType().Name, LoggingStatics.GetMetadata(exception));
                throw;
            }

            return Task.FromResult(Unit.Value);
        }
    }
}
