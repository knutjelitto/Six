namespace SixComp
{
    public partial class ParseTree
    {
        public class InfixCastExpression : BaseExpression, IExpression
        {
            public InfixCastExpression(IExpression left, Token op, IType right)
            {
                Left = left;
                Op = op;
                Right = right;
            }

            public IExpression Left { get; }
            public Token Op { get; }
            public IType Right { get; }

            public override IExpression? LastExpression => null;

            public override string ToString()
            {
                return $"({Left} {Op} {Right})";
            }
        }
    }
}