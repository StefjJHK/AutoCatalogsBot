using VkNet;
using VkNet.Model;
using GoogleSheets.ParametrObjects;
using BusinessLogic.ParametrObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presentation.ParametrObjects;

namespace Presentation.Installers
{
    public class ParametrObjectsInstaller : IServicesInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(services => 
                configuration.GetSection("VkSecrets").Get<VkParams>());
            services.AddSingleton(services =>
                configuration.GetSection("GoogleSheetsParams").Get<GoogleSheetsParams>());
            services.AddSingleton(services => 
                configuration.GetSection("CommandParserSettings").Get<ParserCommandsParams>());
            services.AddSingleton(services => 
                configuration.GetSection("GoogleSheets:Catalog").Get<SheetCatalogParams>());
            services.AddSingleton(services => 
                configuration.GetSection("GoogleSheets:Discussions").Get<SheetDiscussionsParams>());
            services.AddSingleton(services => 
                configuration.GetSection("GoogleSheets:Patterns").Get<SheetPatternsParams>());
            services.AddSingleton(services =>
                configuration.GetSection("CircutBrakerSettings").Get<CircutBrakerParams>());

            var appVkApi = new VkApi();
            appVkApi.Authorize(new ApiAuthParams
            {
                AccessToken = configuration["VkSecrets:AccessAppToken"]
            });

            services.AddSingleton(services => new WallPostsParams(
                appVkApi, ulong.Parse(configuration["VkSecrets:GroupId"])));
        }
    }
}
