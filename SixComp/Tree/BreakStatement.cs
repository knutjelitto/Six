namespace SixComp
{
    public partial class ParseTree
    {
        public class BreakStatement : IStatement
        {
            public BreakStatement(BaseName? label)
            {
                Label = label;
            }

            public BaseName? Label { get; }

            public static BreakStatement Parse(Parser parser)
            {
                parser.Consume(ToKind.KwBreak);

                var label = (BaseName?)null;

                if (parser.Current == ToKind.Name && !parser.CurrentToken.NewlineBefore)
                {
                    label = BaseName.Parse(parser);
                }

                return new BreakStatement(label);
            }

            public override string ToString()
            {
                var label = Label == null ? string.Empty : $" {Label}";
                return $"{Kw.Break}{label}";
            }
        }
    }
}