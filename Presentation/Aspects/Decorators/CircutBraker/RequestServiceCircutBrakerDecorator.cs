using BusinessLogic.Services.Requests;
using Presentation.Aspects.Abstracts;
using System;

namespace Presentation.Aspects.CircutBraker
{
    public class RequestServiceCircutBrakerDecorator<TCommand, TRequest> : IRequestService<TCommand, TRequest>
        where TCommand : class
    {
        private readonly IRequestService<TCommand, TRequest> _decorate;
        private readonly IRequestCircutBraker _braker;

        public RequestServiceCircutBrakerDecorator(IRequestService<TCommand, TRequest> decorate, IRequestCircutBraker braker)
        {
            _decorate = decorate ??
                throw new ArgumentNullException(nameof(decorate));
            _braker = braker ??
                throw new ArgumentNullException(nameof(braker));
        }

        public TRequest Execute(TCommand request)
        {
            return _braker.Execute(() => _decorate.Execute(request));
        }
    }
}
