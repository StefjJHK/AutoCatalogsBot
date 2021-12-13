using VkNet.Abstractions;

namespace Presentation.ParametrObjects
{
    public class WallPostsParams
    {
        public IVkApi Api { get; init; }
        public ulong Id { get; init; }

        public WallPostsParams(IVkApi api, ulong id)
        {
            Api = api;
            Id = id;
        }
    }
}
