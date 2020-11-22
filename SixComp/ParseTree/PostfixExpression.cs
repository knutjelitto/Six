namespace SixComp.ParseTree
{
    public abstract class PostfixExpression : BaseExpression, AnyPostfix
    {
        public PostfixExpression(AnyExpression left, Token op)
        {
            Left = left;
            Op = op;
        }

        public AnyExpression Left { get; }
        public Token Op { get; }
    }
}
