using BusinessLogic.ParamterObjects.Service.Commands;
using BusinessLogic.Utility;
using System;

namespace BusinessLogic.Services.Commands
{
    public class UpdateAdapterService : ICommandService<UpdateAdapterCommand>
    {
        private readonly ICommandService<UpdateCommand> _updateService;
        private readonly CatalogsParser _catalogsParser;

        public UpdateAdapterService(ICommandService<UpdateCommand> updateService, CatalogsParser catalogsParser)
        {
            _updateService = updateService ??
                throw new ArgumentNullException(nameof(updateService));
            _catalogsParser = catalogsParser ??
                throw new ArgumentNullException(nameof(catalogsParser));
        }

        public void Execute(UpdateAdapterCommand command)
        {
            var catalogs = _catalogsParser.Parse(command.Posts);
            _updateService.Execute(new UpdateCommand(catalogs));
        }
    }
}
