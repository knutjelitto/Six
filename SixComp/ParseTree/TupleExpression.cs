namespace SixComp.ParseTree
{
    public class TupleExpression : BaseExpression, AnyPrimary
    {
        private TupleExpression(ExpressionList expressions)
        {
            Expressions = expressions;
        }

        public ExpressionList Expressions { get; }

        public static TupleExpression From(ExpressionList expressions)
        {
            return new TupleExpression(expressions);
        }

        public override string ToString()
        {
            return $"({Expressions})";
        }
    }
}
