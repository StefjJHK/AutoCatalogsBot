using System.Collections.Generic;

namespace BusinessLogic.IRepositories
{
    public interface ISheetsCatalogsRepository
    {
        void Refresh(string sheet, List<IList<object>> catalog);

        Dictionary<string, List<IList<object>>> Get(IEnumerable<string> sheets);
        List<IList<object>> GetBySheet(string sheet);
    }
}
