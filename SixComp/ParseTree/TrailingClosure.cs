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

        public bool BlockOnly => Label == Label.Empty && Closure.BlockOnly;

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
    }
}
