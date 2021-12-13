using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BusinessLogic.IRepositories;
using DataAccess.Repositories.Catalogs;
using DataAccess.Repositories.Discussions;
using DataAccess.Repositories.SearchPatterns;

namespace Presentation.Installers
{
    public class RepositoriesInstaller : IServicesInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ICatalogsRepository, SheetsCatalogsRepositoryAdapter>();
            services.AddSingleton<ISearchPatternsRepository, SheetsSearchPatternsRepositoryAdapter>();
            services.AddSingleton<IDiscussionsRepository, SheetsDiscussionsRepositoryAdapter>();
        }
    }
}
