using MediatR;
using VkNet.Model;

namespace Presentation.Application.Commands.Api
{
    public class ProcessUserRequestCommand : IRequest<string>
    {
        public long PeerId { get; init; }
        public string Text { get; init; }

        public ProcessUserRequestCommand(Message message)
        {
            PeerId = (long)message.PeerId;
            Text = message.Text;
        }
    }
}
