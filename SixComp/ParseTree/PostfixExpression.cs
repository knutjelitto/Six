namespace SixComp.ParseTree
{
    public abstract class PostfixExpression : AnyPostfix
    {
        public PostfixExpression(AnyExpression left)
        {
            Left = left;
        }

        public AnyExpression Left { get; }
    }
}
