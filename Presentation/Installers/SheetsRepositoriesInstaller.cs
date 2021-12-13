using BusinessLogic.IRepositories;
using BusinessLogic.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Installers
{
    public class SheetsRepositoriesInstaller : IServicesInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ISheetsCatalogsRepository, SheetsCatalogsRepository>();
            services.AddSingleton<ISheetsSearchPatternsRepository, SheetsSearchPatternsRepository>();
            services.AddSingleton<ISheetsDiscussionsRepository, SheetsDiscussionsRepository>();
        }
    }
}
