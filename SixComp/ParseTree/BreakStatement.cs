namespace SixComp.ParseTree
{
    public class BreakStatement : AnyStatement
    {
        public BreakStatement(Name? label)
        {
            Label = label;
        }

        public Name? Label { get; }

        public static BreakStatement Parse(Parser parser)
        {
            parser.Consume(ToKind.KwBreak);

            var label = (Name?)null;

            if (parser.Current == ToKind.Name && !parser.CurrentToken.NewlineBefore)
            {
                label = Name.Parse(parser);
            }

            return new BreakStatement(label);
        }
    }
}
