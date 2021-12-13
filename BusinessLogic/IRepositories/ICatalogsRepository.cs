using BusinessLogic.DTO;
using System.Collections.Generic;

namespace BusinessLogic.IRepositories
{
    public interface ICatalogsRepository
    {
        void Refresh(CatalogDTO catalog);
        void RefreshAll(IEnumerable<CatalogDTO> catalogs);

        IEnumerable<CatalogDTO> GetAll();
        CatalogDTO GetByKind(string kind);
    }
}