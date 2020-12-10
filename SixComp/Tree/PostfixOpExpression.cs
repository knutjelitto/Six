using System.Diagnostics;

namespace SixComp
{
    public partial class ParseTree
    {
        public class PostfixOpExpression : PostfixExpression
        {
            public PostfixOpExpression(IExpression left, Token op)
                : base(left, op)
            {
            }

            public static IPostfixExpression Parse(Parser parser, IExpression left)
            {
                Debug.Assert(parser.IsPostfixOperator());
                var op = parser.ConsumeAny();

                return new PostfixOpExpression(left, op);
            }
        }
    }
}