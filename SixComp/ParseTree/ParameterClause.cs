using SixComp.Support;

namespace SixComp.ParseTree
{
    public class ParameterClause
    {
        private ParameterClause(ParameterList parameters, bool variadic)
        {
            Parameters = parameters;
            Variadic = variadic;
        }

        public ParameterList Parameters { get; }
        public bool Variadic { get; }

        public static ParameterClause Parse(Parser parser)
        {
            parser.Consume(ToKind.LParent);

            var parameters = ParameterList.Parse(parser, new TokenSet(ToKind.RParent, ToKind.DotDotDot));
            var variadic = parser.Match("...");
            parser.Consume(ToKind.RParent);

            return new ParameterClause(parameters, variadic);
        }

        public override string ToString()
        {
            var variadic = Variadic ? " ..." : string.Empty;
            return $"({Parameters}{variadic})";
        }
    }
}
