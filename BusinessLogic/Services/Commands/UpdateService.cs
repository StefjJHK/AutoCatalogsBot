using BusinessLogic.IRepositories;
using BusinessLogic.Notifications;
using BusinessLogic.ParametrObjects.Notifications;
using BusinessLogic.ParamterObjects.Service.Commands;
using BusinessLogic.Statics;
using System;
using System.Linq;

namespace BusinessLogic.Services.Commands
{
    public class UpdateService : ICommandService<UpdateCommand>
    {
        private readonly INotificationHandler<CatalogUpdated> _notificationHandler;
        private readonly ICatalogsRepository _catalogsRepository;

        public UpdateService(ICatalogsRepository catalogsRepository, 
            INotificationHandler<CatalogUpdated> notificationHandler)
        {
            _catalogsRepository = catalogsRepository ??
                throw new ArgumentNullException(nameof(catalogsRepository));
            _notificationHandler = notificationHandler ??
                 throw new ArgumentNullException(nameof(notificationHandler));
        }

        public void Execute(UpdateCommand command)
        {
            var storeCatalogs = command.Catalogs
                .Select(catalog => _catalogsRepository
                    .GetByKind(catalog.Discussion.Kind));

            var updatedCatalogs = command.Catalogs
                .Join(storeCatalogs,
                    p => p.Discussion.Kind,
                    t => t.Discussion.Kind,
                    (p, t) => CatalogOperations.CombineCatalogs(p, t));

            foreach (var catalog in updatedCatalogs)
            {
                _catalogsRepository.Refresh(catalog);
            }

            foreach (var catalog in command.Catalogs)
            {
                _notificationHandler.Handle(new CatalogUpdated(catalog));
            }
        }
    }
}
