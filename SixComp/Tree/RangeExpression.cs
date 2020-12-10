namespace SixComp
{
    public partial class ParseTree
    {
        public class RangeExpression : BaseExpression, IExpression
        {
            public RangeExpression(IExpression? left, IExpression? right, bool exclusive)
            {
                Left = left;
                Right = right;
                Exclusive = exclusive;
            }

            public IExpression? Left { get; }
            public IExpression? Right { get; }
            public bool Exclusive { get; }

            public override IExpression? LastExpression => Right?.LastExpression;
        }
    }
}