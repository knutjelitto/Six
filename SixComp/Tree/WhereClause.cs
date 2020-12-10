using SixComp.Support;
using System;

namespace SixComp
{
    public partial class ParseTree
    {
        public class WhereClause
        {
            public static readonly TokenSet Firsts = new TokenSet(ToKind.KwWhere);

            private WhereClause(IExpression expression)
            {
                Expression = expression;
            }

            public IExpression Expression { get; }

            public static WhereClause Parse(Parser parser)
            {
                parser.Consume(ToKind.KwWhere);

                var expression = IExpression.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(WhereClause)}");

                return new WhereClause(expression);
            }

            public override string ToString()
            {
                return $" where {Expression}";
            }
        }
    }
}