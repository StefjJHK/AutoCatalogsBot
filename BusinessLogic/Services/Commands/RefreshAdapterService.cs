using BusinessLogic.ParamterObjects.Service.Commands;
using BusinessLogic.Utility;
using System;

namespace BusinessLogic.Services.Commands
{
    public class RefreshAdapterService : ICommandService<RefreshAdapterCommand>
    {
        private readonly ICommandService<RefreshCommand> _refreshService;
        private readonly CatalogsParser _catalogsParser;

        public RefreshAdapterService(ICommandService<RefreshCommand> refreshService, CatalogsParser catalogsParser)
        {
            _refreshService = refreshService ??
                throw new ArgumentNullException(nameof(refreshService));
            _catalogsParser = catalogsParser ??
                throw new ArgumentNullException(nameof(catalogsParser));
        }

        public void Execute(RefreshAdapterCommand command)
        {
            var catalogs = _catalogsParser.Parse(command.Posts);
            _refreshService.Execute(new RefreshCommand(catalogs));
        }
    }
}
