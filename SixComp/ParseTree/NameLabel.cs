namespace SixComp.ParseTree
{
    public class NameLabel
    {
        public NameLabel(Name name)
        {
            Name = name;
        }

        public Name Name { get; }

        public static NameLabel Parse(Parser parser)
        {
            var name = Name.Parse(parser);
            parser.Consume(ToKind.Colon);

            return new NameLabel(name);
        }

        public override string ToString()
        {
            return $"{Name}: ";
        }
    }
}
