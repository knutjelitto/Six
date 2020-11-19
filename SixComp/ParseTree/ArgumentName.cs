namespace SixComp.ParseTree
{
    public class ArgumentName
    {
        public ArgumentName(Name name)
        {
            Name = name;
        }

        public Name Name { get; }

        public static ArgumentName? TryParse(Parser parser)
        {
            if (parser.Current == ToKind.Name && parser.Next == ToKind.Colon)
            {
                var name = Name.Parse(parser);
                parser.Consume(ToKind.Colon);

                return new ArgumentName(name);
            }

            return null;
        }
    }
}
