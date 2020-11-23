namespace SixComp.ParseTree
{
    public class TryExpression: BaseExpression
    {
        public TryKind Kind { get; }
        public AnyExpression Expression { get; }

        public override AnyExpression? LastExpression => Expression.LastExpression;

        public enum TryKind
        {
            None,

            Try,
            TryForce,
            TryChain,
        }

        public TryExpression(TryKind kind, AnyExpression expression)
        {
            Kind = kind;
            Expression = expression;
        }

        public static TryExpression From(TryKind kind, AnyExpression expression)
        {
            return new TryExpression(kind, expression);
        }
    }
}
