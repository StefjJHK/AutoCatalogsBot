using System.Collections.Generic;
using System.Linq;
using BusinessLogic.DTO;

namespace BusinessLogic.Statics
{
    static class TitlesGroupOperations
    {
        public static bool Contains(IEnumerable<TitlesGroupDTO> titlesGroups, char letter)
        {
            return titlesGroups.
                Any(group => group.Letter == letter);
        }

        public static TitlesGroupDTO GetGroupById(IEnumerable<TitlesGroupDTO> titlesGroups, char letter)
        {
            return titlesGroups.
                FirstOrDefault(group => group.Letter == letter);
        }
    }
}
