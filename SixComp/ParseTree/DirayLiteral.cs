namespace SixComp.ParseTree
{
    public sealed class DirayLiteral : BaseExpression, AnyPrimaryExpression
    {
        public DirayLiteral(DirayLiteralItemList items)
        {
            Items = items;
        }

        public DirayLiteralItemList Items { get; }

        public static DirayLiteral Parse(Parser parser)
        {
            parser.Consume(ToKind.LBracket);

            var items = DirayLiteralItemList.Parse(parser);

            parser.Consume(ToKind.RBracket);

            return new DirayLiteral(items);
        }

        public override string ToString()
        {
            return $"[{Items}]";
        }
    }
}
