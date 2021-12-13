using System;
using System.Linq;
using VkNet.Abstractions;
using VkNet.Model.RequestParams;
using Presentation.ParametrObjects;
using BusinessLogic.ParametrObjects.Notifications;
using BusinessLogic.Notifications;

namespace Presentation.NotificationHandlers.BusinessLogic
{
    public class VkRefreshedNotificationHandler : INotificationHandler<CatalogsRefreshed>
    {
        private readonly IVkApi _iVk;
        private readonly ulong _id;

        public VkRefreshedNotificationHandler(IVkApi iVk, VkParams vkParams)
        {
            _iVk = iVk ??
                 throw new ArgumentNullException(nameof(iVk));
            _id = vkParams?.AdminDiscussion ??
                throw new ArgumentNullException(nameof(vkParams));
        }

        public void Handle(CatalogsRefreshed notification)
        {
            _iVk.Messages.SendAsync(new MessagesSendParams
            {
                RandomId = DateTime.Now.Ticks,
                PeerId = (long?)_id,
                Message = $"Следующие каталоги были обновлены:\n" +
                    $"{string.Join("\n", notification.Catalogs.Select(catalog => catalog.Discussion.Kind))}"
            });
        }
    }
}
