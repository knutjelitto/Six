namespace SixComp
{
    public partial class ParseTree
    {
        public class InfixExpression : BaseExpression, IExpression
        {
            public InfixExpression(IExpression left, Token op, IExpression right)
            {
                Left = left;
                Op = op;
                Right = right;
            }

            public IExpression Left { get; }
            public Token Op { get; }
            public IExpression Right { get; }

            public override IExpression? LastExpression
            {
                get
                {
                    var last = Right.LastExpression;
                    return last;
                }
            }

            public override string ToString()
            {
                return $"({Left} {Op} {Right})";
            }
        }
    }
}