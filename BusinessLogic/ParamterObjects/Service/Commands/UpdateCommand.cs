using BusinessLogic.DTO;
using System.Collections.Generic;

namespace BusinessLogic.ParamterObjects.Service.Commands
{
    public class UpdateCommand
    {
        public IEnumerable<CatalogDTO> Catalogs { get; init; }

        public UpdateCommand(IEnumerable<CatalogDTO> catalogs)
        {
            Catalogs = catalogs;
        }
    }
}
