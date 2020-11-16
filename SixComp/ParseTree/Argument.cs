namespace SixComp.ParseTree
{
    public class Argument
    {
        public Argument(Name? label, AnyExpression value)
        {
            Label = label;
            Value = value;
        }

        public Name? Label { get; }
        public AnyExpression Value { get; }

        public static Argument Parse(Parser parser)
        {
            Name? label = null;

            if (parser.Current.Kind == ToKind.Name && parser.Next.Kind == ToKind.Colon)
            {
                label = Name.Parse(parser);
                parser.Consume(ToKind.Colon);
            }

            var expression = AnyExpression.Parse(parser);

            return new Argument(label, expression);
        }

        public override string ToString()
        {
            var label = Label == null ? string.Empty : $"{Label}: ";

            return $"{label}{Value}";
        }
    }
}
