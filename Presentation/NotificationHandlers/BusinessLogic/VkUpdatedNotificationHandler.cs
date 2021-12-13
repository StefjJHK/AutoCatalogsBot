using System;
using System.Linq;
using VkNet.Abstractions;
using VkNet.Model.RequestParams;
using Presentation.ParametrObjects;
using BusinessLogic.ParametrObjects.Notifications;
using BusinessLogic.Notifications;

namespace Presentation.NotificationHandlers.BusinessLogic
{
    public class VkUpdatedNotificationHandler : INotificationHandler<CatalogUpdated>
    {
        private readonly IVkApi _iVk;
        private readonly ulong _id;

        public VkUpdatedNotificationHandler(IVkApi iVk, VkParams vkParams)
        {
            _iVk = iVk ??
                throw new ArgumentNullException(nameof(iVk));
            _id = vkParams?.AdminDiscussion ??
                throw new ArgumentNullException(nameof(vkParams));
        }

        public void Handle(CatalogUpdated notification)
        {
            //TODO: move to private method
            string titles = string.Join("\n", notification.Catalog.TitlesGroups
                 .Select(group => $"{group.Letter}:\n" + 
                    string.Join("\n", group.Titles.Select(title => $"{title.Name}: {title.Tag} ({title.Count})"))));

            _iVk.Messages.SendAsync(new MessagesSendParams
            {
                RandomId = DateTime.Now.Ticks,
                PeerId = (long?)_id,
                Message = $"В каталог {notification.Catalog.Discussion.Kind} были добавлены тайтлы:\n{titles}"
            });
        }
    }
}
