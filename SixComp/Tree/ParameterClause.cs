using SixComp.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public class ParameterClause
        {
            private static readonly TokenSet rparent = new TokenSet(ToKind.RParent);
            private ParameterClause(ParameterList parameters)
            {
                Parameters = parameters;
            }

            public ParameterList Parameters { get; }
            public bool Missing => Parameters.Missing;

            public static ParameterClause Parse(Parser parser)
            {
                parser.Consume(ToKind.LParent);

                var parameters = ParameterList.Parse(parser, rparent);
                parser.Consume(ToKind.RParent);

                return new ParameterClause(parameters);
            }

            public override string ToString()
            {
                return $"({Parameters})";
            }
        }
    }
}