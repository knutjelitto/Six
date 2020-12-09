namespace SixComp
{
    public partial class Tree
    {
        public class ChainExpression : PostfixExpression
        {
            private ChainExpression(AnyExpression left, Token op) : base(left, op) { }

            public static ChainExpression Parse(Parser parser, AnyExpression left)
            {
                var op = parser.Consume(ToKind.Quest);
                return new ChainExpression(left, op);
            }
        }
    }
}