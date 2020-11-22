using SixComp.Support;

namespace SixComp.ParseTree
{
    public class ClosureParameterClause
    {
        public static readonly TokenSet Firsts = new TokenSet(ToKind.LParent, ToKind.Name);

        public ClosureParameterClause(ClosureParameterList parameters, bool parents, bool variadic)
        {
            Parameters = parameters;
            Parents = parents;
            Variadic = variadic;
        }

        public ClosureParameterClause() : this(new ClosureParameterList(), false, false) { }

        public ClosureParameterList Parameters { get; }
        public bool Parents { get; }
        public bool Variadic { get; }

        public bool Missing => Parameters.Missing;
        public bool NameOnly => !Parents && Parameters.NameOnly;
        public bool Definite => !Missing && !NameOnly;

        public static ClosureParameterClause Parse(Parser parser)
        {
            if (parser.Match(ToKind.LParent))
            {
                var fullParameters = ClosureParameterList.Parse(parser, false);
                var variadic = parser.Match(ToKind.DotDotDot);
                parser.Consume(ToKind.RParent);

                return new ClosureParameterClause(fullParameters, true, variadic);
            }

            var nameParameters = ClosureParameterList.Parse(parser, true);

            return new ClosureParameterClause(nameParameters, false, false);
        }
    }
}
