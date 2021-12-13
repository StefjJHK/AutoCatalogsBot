namespace BusinessLogic.ParametrObjects
{
    class TitleFields
    {
        public string Kind { get; init; }
        public string Name { get; init; }
        public string Tag { get; init; }

        public TitleFields(string kind, string name, string tag)
        {
            Kind = kind;
            Name = name;
            Tag = tag;
        }
    }
}
