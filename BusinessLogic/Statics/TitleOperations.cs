using BusinessLogic.DTO;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Statics
{
    static class TitleOperations
    {
        public static bool Contains(IEnumerable<TitleDTO> titles, string name)
        {
            return titles.
                Any(title => title.Name == name);
        }

        public static TitleDTO GetTitleById(IEnumerable<TitleDTO> titles, string name)
        {
            return titles.
                FirstOrDefault(title => title.Name == name);
        }
    }
}
