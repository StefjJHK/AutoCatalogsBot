using BusinessLogic.DTO;
using System.Collections.Generic;

namespace BusinessLogic.ParamterObjects.Service.Commands
{
    public class RefreshAdapterCommand
    {
        public IEnumerable<PostDTO> Posts { get; init; }

        public RefreshAdapterCommand(IEnumerable<PostDTO> posts)
        {
            Posts = posts;
        }
    }
}
