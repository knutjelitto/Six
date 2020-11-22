namespace SixComp.ParseTree
{
    public class RangeExpression : BaseExpression, AnyExpression
    {
        public RangeExpression(AnyExpression? left, AnyExpression? right, bool exclusive)
        {
            Left = left;
            Right = right;
            Exclusive = exclusive;
        }

        public AnyExpression? Left { get; }
        public AnyExpression? Right { get; }
        public bool Exclusive { get; }

        public override AnyExpression? LastExpression => Right?.LastExpression;
    }
}
