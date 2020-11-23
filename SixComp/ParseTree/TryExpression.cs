namespace SixComp.ParseTree
{
    public class TryExpression: BaseExpression
    {
        public TryExpression(TryOperator @try, AnyExpression expression)
        {
            Try = @try;
            Expression = expression;
        }


        public TryOperator Try { get; }
        public AnyExpression Expression { get; }

        public override AnyExpression? LastExpression => Expression.LastExpression;
    }
}
