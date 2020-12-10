namespace SixComp
{
    public partial class ParseTree
    {
        public class ForceExpression : PostfixExpression
        {
            public ForceExpression(IExpression left, Token op)
                : base(left, op)
            {
            }

            public static ForceExpression Parse(Parser parser, IExpression left)
            {
                var token = parser.Consume(ToKind.Bang);
                return new ForceExpression(left, token);
            }
        }
    }
}