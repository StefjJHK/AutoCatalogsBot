using BusinessLogic.IRepositories;
using BusinessLogic.ParametrObjects.Notifications;
using BusinessLogic.Notifications;
using System;

namespace BusinessLogic.Services.Commands
{
    public class RefreshService : ICommandService<RefreshCommand>
    {
        private readonly INotificationHandler<CatalogsRefreshed> _notificationHandler;
        private readonly ICatalogsRepository _catalogsRepository;

        public RefreshService(ICatalogsRepository catalogsRepository, 
            INotificationHandler<CatalogsRefreshed> notificationHandler)
        {
            _catalogsRepository = catalogsRepository ??
                throw new ArgumentNullException(nameof(catalogsRepository));
            _notificationHandler = notificationHandler ??
                throw new ArgumentNullException(nameof(catalogsRepository));
        }

        public void Execute(RefreshCommand command)
        {
            _catalogsRepository.RefreshAll(command.Catalogs);
            _notificationHandler.Handle(new CatalogsRefreshed(command.Catalogs));
        }
    }
}
