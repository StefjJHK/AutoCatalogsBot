namespace BusinessLogic.DTO
{
    public class PostDTO
    {
        public string Url { get; init; }
        public string Text { get; init; }

        public PostDTO(string url, string text)
        {
            Url = url;
            Text = text;
        }
    }
}
