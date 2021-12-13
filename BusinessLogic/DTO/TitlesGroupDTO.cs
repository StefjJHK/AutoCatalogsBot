using System.Collections.Generic;

namespace BusinessLogic.DTO
{
    public class TitlesGroupDTO
    {
        public char Letter { get; init; }
        public IEnumerable<TitleDTO> Titles { get; init; }

        public TitlesGroupDTO(char letter, IEnumerable<TitleDTO> titles)
        {
            Letter = letter;
            Titles = titles;
        }
    }
}
