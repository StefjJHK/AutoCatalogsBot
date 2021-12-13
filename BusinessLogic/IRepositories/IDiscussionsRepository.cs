using BusinessLogic.DTO;
using System.Collections.Generic;

namespace BusinessLogic.IRepositories
{
    public interface IDiscussionsRepository
    {
        IEnumerable<DiscussionDTO> GetAll();
        DiscussionDTO GetByKind(string kind);
    }
}
