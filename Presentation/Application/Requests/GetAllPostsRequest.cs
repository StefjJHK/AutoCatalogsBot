using MediatR;
using System.Collections.Generic;
using BusinessLogic.DTO;

namespace Presentation.Application.Requests
{
    public class GetAllPostsRequest : IRequest<IEnumerable<PostDTO>>
    {

    }
}
