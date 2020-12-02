using SixComp.Support;

namespace SixComp.Tree
{
    public class ExpressionStatement : AnyStatement
    {
        public ExpressionStatement(AnyExpression expression)
        {
            Expression = expression;
        }

        public AnyExpression Expression { get; }

        public static ExpressionStatement? TryParse(Parser parser)
        {
            var expression = AnyExpression.TryParse(parser);

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
