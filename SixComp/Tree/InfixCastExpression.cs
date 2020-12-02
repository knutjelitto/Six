namespace SixComp.Tree
{
    public class InfixCastExpression : BaseExpression, AnyExpression
    {
        public InfixCastExpression(AnyExpression left, Token op, AnyType right)
        {
            Left = left;
            Op = op;
            Right = right;
        }

        public AnyExpression Left { get; }
        public Token Op { get; }
        public AnyType Right { get; }

        public override AnyExpression? LastExpression => null;

        public override string ToString()
        {
            return $"({Left} {Op} {Right})";
        }
    }
}
