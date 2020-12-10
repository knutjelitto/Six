namespace SixComp
{
    public partial class ParseTree
    {
        public class AvailableCondition : BaseExpression, ICondition
        {
            private AvailableCondition(AtTokenGroup tokens)
            {
                Tokens = tokens;
            }

            public AtTokenGroup Tokens { get; }

            public static AvailableCondition Parse(Parser parser)
            {
                parser.Consume(ToKind.CdAvailable);

                var tokens = AtTokenGroup.Parse(parser, ToKind.LParent, ToKind.RParent);

                return new AvailableCondition(tokens);
            }

            public override string ToString()
            {
                return $"{Tokens}";
            }
        }
    }
}