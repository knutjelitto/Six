namespace SixComp
{
    public partial class Tree
    {
        public class ForceExpression : PostfixExpression
        {
            public ForceExpression(AnyExpression left, Token op)
                : base(left, op)
            {
            }

            public static ForceExpression Parse(Parser parser, AnyExpression left)
            {
                var token = parser.Consume(ToKind.Bang);
                return new ForceExpression(left, token);
            }
        }
    }
}