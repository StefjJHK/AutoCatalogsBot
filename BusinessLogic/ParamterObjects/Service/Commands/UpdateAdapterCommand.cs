using BusinessLogic.DTO;
using System.Collections.Generic;

namespace BusinessLogic.ParamterObjects.Service.Commands
{
    public class UpdateAdapterCommand
    {
        public IEnumerable<PostDTO> Posts { get; init; }

        public UpdateAdapterCommand(IEnumerable<PostDTO> posts)
        {
            Posts = posts;
        }
    }
}
