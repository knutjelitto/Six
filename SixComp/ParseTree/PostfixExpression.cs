using System.Diagnostics;

namespace SixComp.ParseTree
{
    public abstract class PostfixExpression : BaseExpression, AnyPostfixExpression
    {
        public PostfixExpression(AnyExpression left, Token op)
        {
            Left = left;
            Op = op;
        }

        public AnyExpression Left { get; }
        public Token Op { get; }

        public override string ToString()
        {
            return $"{Left}{Op}";
        }
    }
}
