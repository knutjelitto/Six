using SixComp.Common;
using System.Diagnostics;

namespace SixComp
{
    public partial class ParseTree
    {
        public class TryExpression : BaseExpression
        {
            private TryExpression(TryOperator @try, IExpression expression)
            {
                Try = @try;
                Expression = expression;
            }


            public TryOperator Try { get; }
            public IExpression Expression { get; }

            public static TryExpression From(TryOperator @try, IExpression expression)
            {
                Debug.Assert(@try.Kind != TryKind.None);
                return new TryExpression(@try, expression);
            }

            public override IExpression? LastExpression => Expression.LastExpression;
        }
    }
}