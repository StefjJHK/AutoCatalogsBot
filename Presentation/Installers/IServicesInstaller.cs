using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Installers
{
    public interface IServicesInstaller
    {
        void Install(IServiceCollection services, IConfiguration configuration);
    }
}
