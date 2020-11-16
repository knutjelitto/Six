using SixComp.Support;

namespace SixComp.ParseTree
{
    public class ExpressionStatement : AnyStatement
    {
        public ExpressionStatement(AnyExpression expression)
        {
            Expression = expression;
        }

        public AnyExpression Expression { get; }

        public static ExpressionStatement Parse(Parser parser)
        {
            var expression = AnyExpression.Parse(parser);

            return new ExpressionStatement(expression);
        }

        public void Write(IWriter writer)
        {
            writer.WriteLine($"{Expression.StripParents()}");
        }
    }
}
