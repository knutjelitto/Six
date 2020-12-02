namespace SixComp.Tree
{
    public class AvailableCondition : BaseExpression, AnyCondition
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
