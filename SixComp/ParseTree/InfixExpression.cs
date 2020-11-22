namespace SixComp.ParseTree
{
    public class InfixExpression : BaseExpression, AnyExpression
    {
        public InfixExpression(AnyExpression left, Token op, AnyExpression right)
        {
            Left = left;
            Op = op;
            Right = right;
        }

        public AnyExpression Left { get; }
        public Token Op { get; }
        public AnyExpression Right { get; }

        public override AnyExpression? LastExpression
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
