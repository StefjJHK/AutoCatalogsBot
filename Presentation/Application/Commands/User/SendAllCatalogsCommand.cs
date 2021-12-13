using MediatR;

namespace Presentation.Application.Commands.User
{
    public class SendAllCatalogsCommand : IRequest
    {
        public long PeerId { get; init; }

        public SendAllCatalogsCommand(long peerId)
        {
            PeerId = peerId;
        }
    }
}
