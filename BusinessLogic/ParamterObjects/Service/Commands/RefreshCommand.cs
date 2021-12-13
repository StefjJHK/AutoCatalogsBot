using BusinessLogic.DTO;
using System.Collections.Generic;

namespace BusinessLogic.Services.Commands
{
    public class RefreshCommand
    {
        public IEnumerable<CatalogDTO> Catalogs { get; init; }

        public RefreshCommand(IEnumerable<CatalogDTO> catalogs)
        {
            Catalogs = catalogs;
        }
    }
}
