using MediatR;
using Presentation.ParametrObjects.Aspects;
using System;
using System.Threading;
using System.Threading.Tasks;
using VkNet.Abstractions;
using VkNet.Model.RequestParams;

namespace Presentation.NotificationHandlers.Aspects
{
    public class ExceptionNotificationHandler : INotificationHandler<ExceptionNotification>
    {
        private readonly IVkApi _iVk;

        public ExceptionNotificationHandler(IVkApi iVk)
        {
            _iVk = iVk ??
                throw new ArgumentNullException(nameof(iVk));
        }

        public async Task Handle(ExceptionNotification notification, CancellationToken cancellationToken)
        {
            await _iVk.Messages.SendAsync(new MessagesSendParams
            {
                RandomId = DateTime.Now.Ticks,
                PeerId = (long?)notification.Id,
                Message = notification.Message
            });
        }
    }
}
