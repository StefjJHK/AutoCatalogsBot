using BusinessLogic.Notifications;
using BusinessLogic.ParametrObjects.Notifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presentation.NotificationHandlers.BusinessLogic;

namespace Presentation.Installers
{
    public class BusinessLogicNotificationsInstaller : IServicesInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<INotificationHandler<CatalogsRefreshed>, VkRefreshedNotificationHandler>();
            services.AddScoped<INotificationHandler<CatalogUpdated>, VkUpdatedNotificationHandler>();
            services.AddScoped<INotificationHandler<PostParseFailed>, VkParseFailedNotificationHandler>();
        }
    }
}
