using System;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogic.DTO;
using BusinessLogic.ParamterObjects.Service.Commands;
using BusinessLogic.Services.Commands;
using MediatR;
using Presentation.Application.Commands.Api;

namespace Presentation.Application.Handlers.Api
{
    public class UpdatedCatalogCommandHandler : IRequestHandler<UpdatedCatalogCommand>
    {
        private readonly ICommandService<UpdateAdapterCommand> _updateService;

        public UpdatedCatalogCommandHandler(ICommandService<UpdateAdapterCommand> updateSerivce)
        {
            _updateService = updateSerivce ??
                throw new ArgumentNullException(nameof(updateSerivce));
        }

        public async Task<Unit> Handle(UpdatedCatalogCommand request, CancellationToken cancellationToken)
        {
            var command = new UpdateAdapterCommand(new[]
            {
                new PostDTO(request.Url, request.Text)
            });

            _updateService.Execute(command);

            return Task.FromResult(Unit.Value).Result;
        }
    }
}
