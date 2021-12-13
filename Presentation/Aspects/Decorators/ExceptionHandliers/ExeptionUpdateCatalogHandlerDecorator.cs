using Google;
using MediatR;
using Presentation.Application.Commands.Api;
using Presentation.Aspects.Implementation.CircutBraker;
using Presentation.ParametrObjects;
using Presentation.ParametrObjects.Aspects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.Aspects.Decorators.ExceptionHandliers
{
    public class ExeptionUpdateCatalogHandlerDecorator : IRequestHandler<UpdatedCatalogCommand>
    {
        private readonly IMediator _mediator;
        private readonly IRequestHandler<UpdatedCatalogCommand> _decorate;

        private readonly ulong _adminDiscussion;

        public ExeptionUpdateCatalogHandlerDecorator(IMediator mediator, 
            IRequestHandler<UpdatedCatalogCommand> decorate, VkParams vkParams)
        {
            _mediator = mediator ??
                throw new ArgumentNullException(nameof(mediator));
            _decorate = decorate ??
                throw new ArgumentNullException(nameof(decorate));
            _adminDiscussion = vkParams?.AdminDiscussion ??
                throw new ArgumentNullException(nameof(vkParams));
        }

        public async Task<Unit> Handle(UpdatedCatalogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _decorate.Handle(request, cancellationToken);
            }
            catch (GoogleApiException exception)
            {
                var notification = new ExceptionNotification(
                    "Произошла ошибка во время получения данных, база данных недоступна.",
                    _adminDiscussion);

                await _mediator.Publish(notification);
            }
            catch (Exception exception) when (
                exception is CommandCircutBreakerException ||
                exception is RequestCircutBreakerException)
            {
                var notification = new ExceptionNotification(
                    $"Сервер недоступен. Сообщение ошибки: { exception.Message }",
                    _adminDiscussion);

                await _mediator.Publish(notification);
            }

            return Task.FromResult(Unit.Value).Result;
        }
    }
}
