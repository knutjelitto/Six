namespace SixComp.ParseTree
{
    public class WhereClause
    {
        private WhereClause(AnyExpression expression)
        {
            Expression = expression;
        }

        public AnyExpression Expression { get; }

        public static WhereClause Parse(Parser parser)
        {
            parser.Consume(ToKind.KwWhere);

            var expression = AnyExpression.Parse(parser);

            return new WhereClause(expression);
        }

        public override string ToString()
        {
            return $"where {Expression}";
        }
    }
}
