using BusinessLogic.Services.Commands;
using Microsoft.Extensions.Logging;
using Presentation.Aspects.Utility;
using System;

namespace Presentation.Aspects.Logging
{
    public class CommandServiceLoggingDecorator<T> : ICommandService<T> 
        where T : class
    {
        private readonly ICommandService<T> _decorate;
        private readonly ILogger<CommandServiceLoggingDecorator<T>> _logger;

        public CommandServiceLoggingDecorator(ICommandService<T> decorate, ILogger<CommandServiceLoggingDecorator<T>> logger)
        {
            _decorate = decorate ??
                throw new ArgumentNullException(nameof(decorate));
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        public void Execute(T command)
        {
            _logger.LogInformation("[{DateTime}] Handling {CommandName}; {Metadata}", 
                DateTimeOffset.Now, typeof(T).Name, LoggingStatics.GetMetadata(command));

            _decorate.Execute(command);

            _logger.LogInformation("[{DateTime}] Completed handling {CommandName}; {Metadata}", 
                DateTimeOffset.Now, typeof(T).Name, LoggingStatics.GetMetadata(command));
        }
    }
}
