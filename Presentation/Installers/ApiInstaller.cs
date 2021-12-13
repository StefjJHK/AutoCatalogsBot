using BusinessLogic.IRepositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Installers
{
    public class ApiInstaller : IServicesInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson();
            
            services.AddMediatR(typeof(ICatalogsRepository));
        }
    }
}
