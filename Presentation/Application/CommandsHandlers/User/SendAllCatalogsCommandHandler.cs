using BusinessLogic.Models;
using BusinessLogic.Services.Requests;
using BusinessLogic.ParamterObjects.Service.Requests;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using VkNet.Abstractions;
using VkNet.Model.RequestParams;
using Presentation.Application.Commands.User;
using BusinessLogic.DTO;

namespace Presentation.Application.Handlers.User
{
    public class SendAllCatalogsCommandHandler : IRequestHandler<SendAllCatalogsCommand>
    {
        private readonly IVkApi _api;
        private readonly IRequestService<GetAllRequest, IEnumerable<CatalogDTO>> _getAllService;

        public SendAllCatalogsCommandHandler(IVkApi api, IRequestService<GetAllRequest, IEnumerable<CatalogDTO>> getAllService)
        {
            _api = api ?? 
                throw new ArgumentNullException(nameof(api));
            _getAllService = getAllService ??
                 throw new ArgumentNullException(nameof(getAllService));
        }

        public async Task<Unit> Handle(SendAllCatalogsCommand request, CancellationToken cancellationToken)
        {
            var catalogs = _getAllService.Execute(new GetAllRequest());
            var catalogsModelView =  catalogs
                   .Select(catalog => new CatalogModel(catalog));

            foreach (var catalog in catalogsModelView)
            {
                _api.Messages.Send(new MessagesSendParams
                {
                    RandomId = DateTime.Now.Ticks,
                    PeerId = request.PeerId,
                    Message = $"{catalog.Kind}:\n" +
                    string.Join("\n", catalog.TitlesGroups.Select(title => title.SummaryText))
                });
            }

            return await Task.FromResult(Unit.Value);
        }
    }
}
