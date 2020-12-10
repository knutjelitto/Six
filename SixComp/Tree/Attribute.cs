namespace SixComp
{
    public partial class ParseTree
    {
        public class Attribute
        {
            public Attribute(BaseName name, AtTokenGroup? arguments)
            {
                Name = name;
                Arguments = arguments;
            }

            public BaseName Name { get; }
            public AtTokenGroup? Arguments { get; }

            public static Attribute Parse(Parser parser)
            {
                parser.Consume(ToKind.At);

                var name = BaseName.Parse(parser);
                var adjacent = name.Token.Span.End == parser.CurrentToken.Span.Start;
                var arguments = adjacent && parser.Current == ToKind.LParent
                    ? AtTokenGroup.Parse(parser, ToKind.LParent, ToKind.RParent)
                    : null;

                return new Attribute(name, arguments);
            }

            public static Attribute From(Token keyword)
            {
                return new Attribute(BaseName.From(keyword), null);
            }

            public override string ToString()
            {
                return $"@{Name}{Arguments}";
            }
        }
    }
}