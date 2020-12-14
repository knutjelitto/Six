using Six.Support;
using SixComp.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public class ExpressionStatement : IStatement
        {
            public ExpressionStatement(IExpression expression)
            {
                Expression = expression;
            }

            public IExpression Expression { get; }

            public static ExpressionStatement? TryParse(Parser parser)
            {
                var expression = IExpression.TryParse(parser);

                if (expression == null)
                {
                    return null;
                }

                return new ExpressionStatement(expression);
            }

            public void Write(IWriter writer)
            {
                writer.WriteLine($"{Expression.StripParents()}");
            }

            public override string ToString()
            {
                return $"{Expression}";
            }
        }
    }
}