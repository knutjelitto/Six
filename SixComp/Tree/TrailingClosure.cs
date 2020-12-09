namespace SixComp
{
    public partial class Tree
    {
        public class TrailingClosure
        {
            private TrailingClosure(Label? label, ClosureExpression closure)
            {
                Label = label;
                Closure = closure;
            }

            public Label? Label { get; }
            public ClosureExpression Closure { get; }

            public bool BlockOnly => Label == null && Closure.BlockOnly;

            public static TrailingClosure? TryParse(Parser parser, bool first)
            {
                var label = first ? null : Label.Parse(parser, true);
                var closure = ClosureExpression.TryParse(parser);

                if (closure == null)
                {
                    return null;
                }

                return new TrailingClosure(label, closure);
            }

            public override string ToString()
            {
                return $"{Label}{Closure}";
            }
        }
    }
}