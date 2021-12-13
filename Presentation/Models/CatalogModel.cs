using BusinessLogic.DTO;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Models
{
    public class CatalogModel
    {
        public long Id { get; init; }
        public string Kind { get; init; }

        public IEnumerable<TitlesGroupModel> TitlesGroups { get; init; }
        
        public CatalogModel(CatalogDTO catalogTDO)
        {
            Id = catalogTDO.Discussion.Id;
            Kind = catalogTDO.Discussion.Kind;

            TitlesGroups = catalogTDO.TitlesGroups
                .Select(group => new TitlesGroupModel(group));
        }
    }
}