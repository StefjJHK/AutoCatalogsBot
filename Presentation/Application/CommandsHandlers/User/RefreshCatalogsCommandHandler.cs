using BusinessLogic.Models;
using BusinessLogic.Services.Commands;
using BusinessLogic.ParamterObjects.Service.Requests;
using BusinessLogic.ParamterObjects.Service.Commands;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VkNet.Abstractions;
using VkNet.Model.RequestParams;
using System.Collections.Generic;
using BusinessLogic.Services.Requests;
using Presentation.Application.Requests;
using Presentation.Application.Commands.User;
using BusinessLogic.DTO;

namespace Presentation.Application.Handlers.User
{
    public class RefreshCatalogsCommandHandler : IRequestHandler<RefreshCatalogsCommand>
    {
        private readonly IVkApi _api;
        private readonly IMediator _mediator;

        private readonly IRequestService<GetAllRequest, IEnumerable<CatalogDTO>> _getAllService;
        private readonly ICommandService<RefreshAdapterCommand> _refreshService;

        public RefreshCatalogsCommandHandler(IVkApi api, IMediator mediator,
            IRequestService<GetAllRequest, IEnumerable<CatalogDTO>> getAllService, ICommandService<RefreshAdapterCommand> refreshService)
        {
            _api = api ?? 
                throw new ArgumentNullException(nameof(api));
            _mediator = mediator ??
                throw new ArgumentNullException(nameof(mediator));
            _getAllService = getAllService ??
                throw new ArgumentNullException(nameof(getAllService));
            _refreshService = refreshService ??
                throw new ArgumentNullException(nameof(refreshService));
        }

        public async Task<Unit> Handle(RefreshCatalogsCommand request, CancellationToken cancellationToken)
        {
            var getRequest = new GetAllPostsRequest();
            var result = _mediator.Send(getRequest).Result;

            _refreshService.Execute(new RefreshAdapterCommand(result));

            var catalogsModelViews = _getAllService.Execute(new GetAllRequest())
                .Select(catalog => new CatalogModel(catalog));

            foreach (var catalog in catalogsModelViews)
            {
                RefreshCatalog(catalog);
            }

            return await Task.FromResult(Unit.Value);
        }

        private void RefreshCatalog(CatalogModel catalogView)
        {
            _api.Messages.Send(new MessagesSendParams
            {
                RandomId = DateTime.Now.Ticks,
                PeerId = catalogView.Id,
                Message = $"{catalogView.Kind}:\n" +
                    string.Join("\n", catalogView.TitlesGroups.Select(title => title.SummaryText))
            });
        }
    }
}
