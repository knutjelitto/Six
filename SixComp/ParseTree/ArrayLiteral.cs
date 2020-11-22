namespace SixComp.ParseTree
{
    public sealed class ArrayLiteral : BaseExpression, AnyPrimary
    {
        public ArrayLiteral(ArrayLiteralItemList items)
        {
            Items = items;
        }

        public ArrayLiteralItemList Items { get; }

        public static ArrayLiteral Parse(Parser parser)
        {
            parser.Consume(ToKind.LBracket);

            var items = ArrayLiteralItemList.Parse(parser);

            parser.Consume(ToKind.RBracket);

            return new ArrayLiteral(items);
        }

        public override string ToString()
        {
            return $"[{Items}]";
        }
    }
}
