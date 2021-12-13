using System.Collections.Generic;
using BusinessLogic.Utility;

namespace BusinessLogic.IRepositories
{
    public interface ISearchPatternsRepository
    {
        IEnumerable<SearchPatternDTO> GetAll();
    }
}
