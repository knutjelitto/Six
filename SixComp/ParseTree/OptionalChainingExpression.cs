namespace SixComp.ParseTree
{
    public class OptionalChainingExpression : PostfixExpression
    {
        private OptionalChainingExpression(AnyExpression left) : base(left) { }

        public static OptionalChainingExpression Parse(Parser parser, AnyExpression left)
        {
            parser.Consume(ToKind.Quest);
            return new OptionalChainingExpression(left);
        }

        public static OptionalChainingExpression From(AnyExpression left)
        {
            return new OptionalChainingExpression(left);
        }
    }
}
