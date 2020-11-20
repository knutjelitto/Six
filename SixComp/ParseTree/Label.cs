namespace SixComp.ParseTree
{
    public class Label
    {
        public static readonly Label Empty = new Label(null);

        public Label(Name? name)
        {
            Name = name;
        }

        public Name? Name { get; }

        public static Label Parse(Parser parser, bool force)
        {
            if (parser.Current == ToKind.Name && parser.Next == ToKind.Colon || force)
            {
                var name = Name.Parse(parser);
                parser.Consume(ToKind.Colon);
                return new Label(name);
            }

            return new Label(null);
        }

        public override string ToString()
        {
            return Name == null ? string.Empty : $"{Name}: ";
        }
    }
}
