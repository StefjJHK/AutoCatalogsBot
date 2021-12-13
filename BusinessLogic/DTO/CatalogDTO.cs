using System.Collections.Generic;

namespace BusinessLogic.DTO
{
    public class CatalogDTO
    {
        public DiscussionDTO Discussion{ get; init; } 
        public IEnumerable<TitlesGroupDTO> TitlesGroups { get; init; }

        public CatalogDTO(DiscussionDTO discussion, IEnumerable<TitlesGroupDTO> titlesGroups)
        {
            Discussion = discussion;
            TitlesGroups = titlesGroups;
        }
    }
}
