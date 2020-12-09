using System.Diagnostics;

namespace SixComp
{
    public partial class Tree
    {
        public class PostfixOpExpression : PostfixExpression
        {
            public PostfixOpExpression(AnyExpression left, Token op)
                : base(left, op)
            {
            }

            public static AnyPostfixExpression Parse(Parser parser, AnyExpression left)
            {
                Debug.Assert(parser.IsPostfixOperator());
                var op = parser.ConsumeAny();

                return new PostfixOpExpression(left, op);
            }
        }
    }
}