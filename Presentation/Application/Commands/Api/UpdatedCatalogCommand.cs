using MediatR;
using VkNet.Model.GroupUpdate;

namespace Presentation.Application.Commands.Api
{
    public class UpdatedCatalogCommand : IRequest
    {
        public string Url { get; init; }
        public string Text { get; init; }

        public UpdatedCatalogCommand(WallPost post)
        {
            Url = $"https://vk.com/club{-post.OwnerId}?w=wall{post.OwnerId}_{post.Id}/all";
            Text = post.Text;
        }
    }
}
