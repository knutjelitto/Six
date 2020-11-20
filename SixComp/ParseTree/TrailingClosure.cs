using SixComp.Support;

namespace SixComp.ParseTree
{
    public class TrailingClosure
    {
        private TrailingClosure(Label label, ClosureExpression closure)
        {
            Label = label;
            Closure = closure;
        }

        public Label Label { get; }
        public ClosureExpression Closure { get; }

        public static bool Try(Parser parser)
        {
            var offset = parser.Offset;
            var closure = TryParse(parser, true);
            parser.Offset = offset;

            return closure != null;
        }

        public static TrailingClosure? TryParse(Parser parser, bool first)
        {
            var label = first ? Label.Empty : Label.Parse(parser, true);
            var closure = ClosureExpression.TryParse(parser);

            if (closure == null)
            {
                return null;
            }

            return new TrailingClosure(label, closure);
        }

        public static TrailingClosure Parse(Parser parser, bool first)
        {
            var offset = parser.Offset;

            var trailingClosure = TryParse(parser, first);

            if (trailingClosure == null)
            {
                var token = parser.CurrentToken;
                parser.Offset = offset;

                throw new ParserException(token, $"failed to parse `trailing closure` at `{token.Kind.GetRep()}`");
            }

            return trailingClosure;

        }
    }
}
