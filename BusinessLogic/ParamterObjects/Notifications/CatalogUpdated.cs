using BusinessLogic.DTO;

namespace BusinessLogic.ParametrObjects.Notifications
{
    public class CatalogUpdated
    {
        public CatalogDTO Catalog { get; init; }

        public CatalogUpdated(CatalogDTO catalog)
        {
            Catalog = catalog;
        }
    }
}
