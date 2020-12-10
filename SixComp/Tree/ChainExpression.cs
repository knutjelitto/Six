namespace SixComp
{
    public partial class ParseTree
    {
        public class ChainExpression : PostfixExpression
        {
            private ChainExpression(IExpression left, Token op) : base(left, op) { }

            public static ChainExpression Parse(Parser parser, IExpression left)
            {
                var op = parser.Consume(ToKind.Quest);
                return new ChainExpression(left, op);
            }
        }
    }
}