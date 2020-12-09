using SixComp.Common;
using System.Diagnostics;

namespace SixComp
{
    public partial class Tree
    {
        public class TryExpression : BaseExpression
        {
            private TryExpression(TryOperator @try, AnyExpression expression)
            {
                Try = @try;
                Expression = expression;
            }


            public TryOperator Try { get; }
            public AnyExpression Expression { get; }

            public static TryExpression From(TryOperator @try, AnyExpression expression)
            {
                Debug.Assert(@try.Kind != TryKind.None);
                return new TryExpression(@try, expression);
            }

            public override AnyExpression? LastExpression => Expression.LastExpression;
        }
    }
}