using System.Collections.Generic;
using BusinessLogic.DTO;

namespace BusinessLogic.ParametrObjects.Notifications
{
    public class CatalogsRefreshed
    {
        public IEnumerable<CatalogDTO> Catalogs { get; init; }

        public CatalogsRefreshed(IEnumerable<CatalogDTO> catalogs)
        {
            Catalogs = catalogs;
        }
    }
}
