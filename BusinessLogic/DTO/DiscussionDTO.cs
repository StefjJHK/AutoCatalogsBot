namespace BusinessLogic.DTO
{
    public class DiscussionDTO
    {
        public string Kind { get; set; }
        public long Id { get; set; }

        public DiscussionDTO(string kind, long id)
        {
            Kind = kind;
            Id = id;
        }
    }
}
