namespace BusinessLogic.Utility
{
    public class SearchPatternDTO
    {
        public string PatternKind { get; init; }
        public string PatternName { get; init; }
        public string PatternTag { get; init; }

        public SearchPatternDTO(string patternKind, string patternName, string patternTag)
        {
            PatternKind = patternKind;
            PatternName = patternName;
            PatternTag = patternTag;
        }
    }
}
