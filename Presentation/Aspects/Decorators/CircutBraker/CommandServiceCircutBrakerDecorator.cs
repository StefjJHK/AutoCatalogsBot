using BusinessLogic.Services.Commands;
using Presentation.Aspects.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Aspects.CircutBraker
{
    public class CommandServiceCircutBrakerDecorator<TCommand> : ICommandService<TCommand> 
        where TCommand : class
    {
        private readonly ICommandCircutBraker _bracker; 
        private readonly ICommandService<TCommand> _decorate;

        public CommandServiceCircutBrakerDecorator(ICommandCircutBraker bracker, ICommandService<TCommand> decorate)
        {
            _bracker = bracker ??
                throw new ArgumentNullException(nameof(bracker));
            _decorate = decorate ??
                throw new ArgumentNullException(nameof(decorate));
        }

        public void Execute(TCommand command)
        {
            _bracker.Execute(() => _decorate.Execute(command));
        }
    }
}
