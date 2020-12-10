namespace SixComp
{
    public partial class ParseTree
    {
        public class TernaryExpression : BaseExpression, IExpression
        {
            public TernaryExpression(IExpression condition, IExpression whenTrue, IExpression whenFalse)
            {
                Condition = condition;
                WhenTrue = whenTrue;
                WhenFalse = whenFalse;
            }

            public IExpression Condition { get; }
            public IExpression WhenTrue { get; }
            public IExpression WhenFalse { get; }

            public static TernaryExpression From(IExpression condition, IExpression whenTrue, IExpression whenFalse)
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