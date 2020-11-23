using System.Diagnostics;

namespace SixComp.ParseTree
{
    public class PostfixOpExpression : PostfixExpression
    {
        public PostfixOpExpression(AnyExpression left, Token op)
            : base(left, op)
        {
        }

        public static AnyPostfix Parse(Parser parser, AnyExpression left)
        {
            Debug.Assert(parser.IsPostfix());
            var op = parser.ConsumeAny();

            return new PostfixOpExpression(left, op);
        }
    }
}
