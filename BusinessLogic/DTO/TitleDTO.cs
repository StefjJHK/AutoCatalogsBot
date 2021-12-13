namespace BusinessLogic.DTO
{
    public class TitleDTO
    {
        public string Name { get; init; }
        public string Tag { get; init; }
        public int Count { get; init; }

        public TitleDTO(string name, string tag, int count)
        {
            Name = name;
            Tag = tag;
            Count = count;
        }
    }
}
