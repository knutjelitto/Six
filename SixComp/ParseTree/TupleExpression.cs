namespace SixComp.ParseTree
{
    public class TupleExpression : BaseExpression, AnyPrimaryExpression
    {
        private TupleExpression(TupleElementList elements)
        {
            Elements = elements;
        }

        public TupleElementList Elements { get; }

        public static TupleExpression From(TupleElementList elements)
        {
            return new TupleExpression(elements);
        }

        public override string ToString()
        {
            return $"({Elements})";
        }
    }
}
