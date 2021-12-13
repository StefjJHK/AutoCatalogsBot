using BusinessLogic.DTO;

namespace BusinessLogic.ParametrObjects.Notifications
{
    public class PostParseFailed 
    {
        public PostDTO Post { get; init; }

        public PostParseFailed(PostDTO post)
        {
            Post = post;
        }
    }
}
