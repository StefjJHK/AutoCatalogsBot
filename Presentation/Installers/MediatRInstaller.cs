using BusinessLogic;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Application.Commands.Api;
using Presentation.Aspects.Decorators.ExceptionHandliers;
using Presentation.Aspects.Decorators.Logging;
using Presentation.Aspects.Logging;
using Scrutor;

namespace Presentation.Installers
{
    public class MediatRInstaller : IServicesInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.Scan(selector => selector
                .FromAssemblyOf<Startup>()
                    .AddClasses(classes => classes
                        .AssignableToAny(typeof(IRequestHandler<,>), typeof(IRequestHandler<>))
                            .Where(type =>
                                type.Name.EndsWith("RequestHandler") ||
                                type.Name.EndsWith("CommandHandler")))
                        .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()

                    .AddClasses(classes => classes
                        .AssignableToAny(typeof(INotificationHandler<>))
                            .Where(type => type.Name.EndsWith("NotificationHandler")))
                        .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());

            services.Decorate(
                typeof(IRequestHandler<ProcessUserRequestCommand, string>),
                typeof(ExeptionProcessUserRequstHandlerDecorator));
            services.Decorate(
                typeof(IRequestHandler<UpdatedCatalogCommand>),
                typeof(ExeptionUpdateCatalogHandlerDecorator));
            services.Decorate(
                typeof(INotificationHandler<>),
                typeof(NotificationLoggingDecorator<>));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(BehaviourLoggingDecorator<,>));
        }
    }
}
