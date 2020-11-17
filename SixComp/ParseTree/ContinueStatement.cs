namespace SixComp.ParseTree
{
    public class ContinueStatement : AnyStatement
    {
        public ContinueStatement(Name? label)
        {
            Label = label;
        }

        public Name? Label { get; }

        public static BreakStatement Parse(Parser parser)
        {
            parser.Consume(ToKind.KwContinue);

            var label = (Name?)null;

            if (parser.Current == ToKind.Name && !parser.CurrentToken.NewLine)
            {
                label = Name.Parse(parser);
            }

            return new BreakStatement(label);
        }
    }
}
