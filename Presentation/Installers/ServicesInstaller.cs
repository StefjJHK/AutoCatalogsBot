using BusinessLogic.Services.Commands;
using BusinessLogic.Services.Requests;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using GoogleSheets.ParametrObjects;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Aspects.Logging;
using Presentation.Aspects.CircutBraker;
using BusinessLogic.IRepositories;

namespace Presentation.Installers
{
    public class ServicesInstaller : IServicesInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(services => new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = GoogleCredential
                    .FromJson(JsonConvert
                        .SerializeObject(configuration
                            .GetSection("GoogleCredentional").Get<GoogleSheetsCredentional>())),
                ApplicationName = configuration["GoogleSheetsParams:AppName"]
            }));

            services.Scan(selector => selector
                .FromAssemblyOf<ICatalogsRepository>()
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IRequestService<,>))
                            .Where(type => type.Name.EndsWith("Service")))
                        .UsingRegistrationStrategy(Scrutor.RegistrationStrategy.Throw)
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()

                    .AddClasses(classes => classes
                        .AssignableTo(typeof(ICommandService<>))
                            .Where(type => type.Name.EndsWith("Service")))
                        .UsingRegistrationStrategy(Scrutor.RegistrationStrategy.Throw)
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());

            services.Decorate(typeof(ICommandService<>), typeof(CommandServiceCircutBrakerDecorator<>));
            services.Decorate(typeof(ICommandService<>), typeof(CommandServiceLoggingDecorator<>));

            services.Decorate(typeof(IRequestService<,>), typeof(RequestServiceCircutBrakerDecorator<,>));
            services.Decorate(typeof(IRequestService<,>), typeof(RequestServiceLoggingDecorator<,>));
        }
    }
}