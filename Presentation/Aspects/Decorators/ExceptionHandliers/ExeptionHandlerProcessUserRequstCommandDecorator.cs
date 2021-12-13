using Google;
using MediatR;
using Presentation.Application.Commands.Api;
using Presentation.Aspects.Implementation.CircutBraker;
using Presentation.ParametrObjects.Aspects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.Aspects.Decorators.ExceptionHandliers
{
    public class ExeptionProcessUserRequstHandlerDecorator : IRequestHandler<ProcessUserRequestCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IRequestHandler<ProcessUserRequestCommand, string> _decorate;

        public ExeptionProcessUserRequstHandlerDecorator(IMediator mediator, 
            IRequestHandler<ProcessUserRequestCommand, string> decorate)
        {
            _mediator = mediator ??
                throw new ArgumentNullException(nameof(mediator));
            _decorate = decorate ??
                throw new ArgumentNullException(nameof(decorate));
        }

        public async Task<string> Handle(ProcessUserRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _decorate.Handle(request, cancellationToken);
            }
            catch (GoogleApiException exception)
            {
                var notification = new ExceptionNotification(
                    "Произошла ошибка во время получения данных, попробуйте повторить запрос позже.",
                    (ulong)request.PeerId);

                await _mediator.Publish(notification);
            }
            catch (Exception exception) when (
                exception is CommandCircutBreakerException || 
                exception is RequestCircutBreakerException)
            {
                var notification = new ExceptionNotification(
                    "На данный момент сервер недоступен, попробуйте отправить запрос позже.",
                    (ulong)request.PeerId);

                await _mediator.Publish(notification);
            }

            return Task.FromResult("ok").Result;
        }
    }
}
