using Presentation.ParametrObjects;
using Presentation.Application.Utility;
using Presentation.Application.Commands.Api;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Presentation.Application.Commands.User;

namespace Presentation.Application.Handlers.Api
{
    public class ProcessUserRequestCommandHandler : IRequestHandler<ProcessUserRequestCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly ParserCommandsParams _commandsParserSettings;

        public ProcessUserRequestCommandHandler(IMediator mediator, ParserCommandsParams commandsParserSettings)
        {
            _mediator = mediator ?? 
                throw new ArgumentNullException(nameof(mediator));
            _commandsParserSettings = commandsParserSettings ??
                throw new ArgumentNullException(nameof(commandsParserSettings));
        }

        public async Task<string> Handle(ProcessUserRequestCommand request, CancellationToken cancellationToken)
        {
            string cmd = new CommandParser(_commandsParserSettings.CommandPattern,
                                           _commandsParserSettings.CommandIdentity).ExtractCommand(request.Text);
            
            if (cmd == "обновить каталоги")
            {
                var command = new RefreshCatalogsCommand();

               await  _mediator.Send(command, cancellationToken);
            }
            else if (cmd == "каталоги")
            {
                var command = new SendAllCatalogsCommand(request.PeerId);

                await _mediator.Send(command, cancellationToken);
            }

            return Task.FromResult("ok").Result;
        }
    }
}