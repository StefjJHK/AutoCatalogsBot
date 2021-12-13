using BusinessLogic.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Aspects.Abstracts;
using Presentation.Aspects.Implementation.CircutBraker;
using VkNet;
using VkNet.Model;
using VkNet.Abstractions;

namespace Presentation.Installers
{
    public class UtilityInstaller : IServicesInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            var groupVkApi = new VkApi();
            groupVkApi.Authorize(new ApiAuthParams
            {
                AccessToken = configuration["VkSecrets:AccessGroupToken"]
            });

            services.AddSingleton<IVkApi>(value => groupVkApi);
            services.AddTransient<CatalogsParser, CatalogsParser>();

            services.AddSingleton<ICommandCircutBraker, CommandCircutBraker>();
            services.AddSingleton<IRequestCircutBraker, RequestCircutBraker>();
        }
    }
}
