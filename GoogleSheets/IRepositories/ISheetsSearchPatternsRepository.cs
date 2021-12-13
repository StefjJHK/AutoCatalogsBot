using System.Collections.Generic;

namespace BusinessLogic.IRepositories
{
    public interface ISheetsSearchPatternsRepository
    {
        List<IList<object>> Get(string sheet);
    }
}
