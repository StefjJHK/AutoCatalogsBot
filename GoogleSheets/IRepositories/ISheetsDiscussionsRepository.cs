using System.Collections.Generic;

namespace BusinessLogic.IRepositories
{
    public interface ISheetsDiscussionsRepository
    {
        public List<IList<object>> Get(string sheet);
    }
}
