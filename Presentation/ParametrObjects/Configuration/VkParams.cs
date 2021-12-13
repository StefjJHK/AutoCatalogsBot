namespace Presentation.ParametrObjects
{
    public class VkParams
    {
        public ulong GroupId { get; set; }
        public ulong AdminDiscussion { get; set; }

        public string AccessGroupToken { get; set; }
        public string AccessAppToken { get; set; }

        public string Secret { get; set; }
        public string Confirmation { get; set; }
    }
}
