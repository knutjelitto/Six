using SixComp.Support;
using System;

namespace SixComp.ParseTree
{
    public class WhereClause
    {
        public static readonly TokenSet Firsts = new TokenSet(ToKind.KwWhere);

        private WhereClause(AnyExpression expression)
        {
            Expression = expression;
        }

        public AnyExpression Expression { get; }

        public static WhereClause Parse(Parser parser)
        {
            parser.Consume(ToKind.KwWhere);

            var expression = AnyExpression.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(WhereClause)}");

            return new WhereClause(expression);
        }

        public override string ToString()
        {
            return $"where {Expression}";
        }
    }
}
