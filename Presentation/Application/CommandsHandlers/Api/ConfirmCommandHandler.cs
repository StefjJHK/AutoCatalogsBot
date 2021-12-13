using System;
using System.Threading;
using System.Threading.Tasks;
using Presentation.ParametrObjects;
using MediatR;
using Presentation.Application.Commands.Api;

namespace Presentation.Application.Handlers.Api
{
    public class ConfirmCommandHandler : IRequestHandler<ConfirmCommand, string>
    {
        private readonly string ConfirmationString;

        public ConfirmCommandHandler(VkParams vkParams)
        {
            ConfirmationString = vkParams?.Confirmation ?? 
                throw new ArgumentNullException(nameof(vkParams));
        }

        public async Task<string> Handle(ConfirmCommand request, CancellationToken cancellationToken)
        {
            return ConfirmationString;
        }
    }
}
