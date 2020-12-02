namespace SixComp.Tree
{
    public class ContinueStatement : AnyStatement
    {
        public ContinueStatement(BaseName? label)
        {
            Label = label;
        }

        public BaseName? Label { get; }

        public static BreakStatement Parse(Parser parser)
        {
            parser.Consume(ToKind.KwContinue);

            var label = (BaseName?)null;

            if (parser.Current == ToKind.Name && !parser.CurrentToken.NewlineBefore)
            {
                label = BaseName.Parse(parser);
            }

            return new BreakStatement(label);
        }
    }
}
