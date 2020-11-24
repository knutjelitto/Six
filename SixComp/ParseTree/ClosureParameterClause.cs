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

        public static ClosureParameterClause? TryParse(Parser parser)
        {
            var offset = parser.Offset;

            if (parser.Match(ToKind.LParent))
            {
                var fullParameters = ClosureParameterList.TryParse(parser, false);
                if (fullParameters == null)
                {
                    parser.Offset = offset;
                    return null;
                }
                var variadic = parser.Match(ToKind.DotDotDot);
                if (!parser.Match(ToKind.RParent))
                {
                    parser.Offset = offset;
                    return null;
                }

                return new ClosureParameterClause(fullParameters, true, variadic);
            }

            var nameParameters = ClosureParameterList.TryParse(parser, true);

            if (nameParameters == null)
            {
                parser.Offset = offset;
                return null;
            }

            return new ClosureParameterClause(nameParameters, false, false);
        }
    }
}
