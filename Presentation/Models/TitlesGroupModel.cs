using BusinessLogic.DTO;
using System.Linq;

namespace BusinessLogic.Models
{
    public class TitlesGroupModel
    {
        public string SummaryText { get; init; }
        
        public TitlesGroupModel(TitlesGroupDTO group)
        {
            SummaryText = $"{group.Letter}\n";
            SummaryText += string.Join("\n", group.Titles
                .Select(title => title.Tag + " - " + title.Name + $"({title.Count})"));
        }
    }
}
