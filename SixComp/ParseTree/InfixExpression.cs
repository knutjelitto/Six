namespace SixComp.ParseTree
{
    public class InfixExpression : AnyExpression
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

        public override string ToString()
        {
            return $"({Left} {Op} {Right})";
        }
    }
}
