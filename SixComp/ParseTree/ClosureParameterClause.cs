using SixComp.Support;

namespace SixComp.ParseTree
{
    public class ClosureParameterClause
    {
        public static readonly TokenSet Firsts = new TokenSet(ToKind.LParent, ToKind.Name);

        public ClosureParameterClause(ClosureParameterList parameters, bool variadic)
        {
            Parameters = parameters;
            Variadic = variadic;
        }

        public ClosureParameterList Parameters { get; }
        public bool Variadic { get; }

        public static ClosureParameterClause Parse(Parser parser)
        {
            if (parser.Match(ToKind.LParent))
            {
                var fullParameters = ClosureParameterList.Parse(parser, false);
                var variadic = parser.Match(ToKind.DotDotDot);
                parser.Consume(ToKind.RParent);

                return new ClosureParameterClause(fullParameters, variadic);
            }

            var nameParameters = ClosureParameterList.Parse(parser, true);

            return new ClosureParameterClause(nameParameters, false);
        }
    }
}
