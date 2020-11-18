namespace SixComp.ParseTree
{
    public class Attribute
    {
        public Attribute(Name name, AtTokenGroup? arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        public Name Name { get; }
        public AtTokenGroup? Arguments { get; }

        public static Attribute Parse(Parser parser)
        {
            parser.Consume(ToKind.At);

            var name = Name.Parse(parser);
            var adjacent = name.Token.Span.End == parser.CurrentToken.Span.Start;
            var arguments = adjacent && parser.Current == ToKind.LParent
                ? AtTokenGroup.Parse(parser, ToKind.LParent, ToKind.RParent)
                : null;

            return new Attribute(name, arguments);
        }
    }
}
