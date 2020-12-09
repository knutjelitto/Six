namespace SixComp
{
    public partial class Tree
    {
        public class TernaryExpression : BaseExpression, AnyExpression
        {
            public TernaryExpression(AnyExpression condition, AnyExpression whenTrue, AnyExpression whenFalse)
            {
                Condition = condition;
                WhenTrue = whenTrue;
                WhenFalse = whenFalse;
            }

            public AnyExpression Condition { get; }
            public AnyExpression WhenTrue { get; }
            public AnyExpression WhenFalse { get; }

            public static TernaryExpression From(AnyExpression condition, AnyExpression whenTrue, AnyExpression whenFalse)
            {
                return new TernaryExpression(condition, whenTrue, whenFalse);
            }

            public override string ToString()
            {
                return $"({Condition} ? {WhenTrue} : {WhenFalse})";
            }
        }
    }
}