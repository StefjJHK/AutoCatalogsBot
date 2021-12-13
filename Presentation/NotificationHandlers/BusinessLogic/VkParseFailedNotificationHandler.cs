using System;
using VkNet.Abstractions;
using VkNet.Model.RequestParams;
using Presentation.ParametrObjects;
using BusinessLogic.ParametrObjects.Notifications;
using BusinessLogic.Notifications;

//TODO: REname BusinessLogic to something like BusinessLogicNotification
namespace Presentation.NotificationHandlers.BusinessLogic
{
    public class VkParseFailedNotificationHandler : INotificationHandler<PostParseFailed>
    {
        private readonly IVkApi _iVk;
        private readonly ulong _id;

        public VkParseFailedNotificationHandler(IVkApi iVk, VkParams vkParams)
        {
            _iVk = iVk ?? 
                throw new ArgumentNullException(nameof(iVk));
            _id = vkParams?.AdminDiscussion ??
                throw new ArgumentNullException(nameof(vkParams));
        }

        public void Handle(PostParseFailed notification)
        {
            _iVk.Messages.SendAsync(new MessagesSendParams
            {
                RandomId = DateTime.Now.Ticks,
                PeerId = (long?)_id,
                Message = $"Не удалось извлечь все поля из поста {notification.Post.Url}"
            });
        }
    }
}
