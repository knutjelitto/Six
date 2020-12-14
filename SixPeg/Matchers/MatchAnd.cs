using Six.Support;
using SixPeg.Expression;

namespace SixPeg.Matchers
{
    public class MatchAnd : AnyMatcher
    {
        public MatchAnd(AnyExpression expression)
        {
            Expression = expression;
        }

        public AnyExpression Expression { get; }

        public override bool Match(string subject, ref int cursor)
        {
            var start = cursor;
            if (Expression.GetMatcher().Match(subject, ref cursor))
            {
                cursor = start;
                return true;
            }

            cursor = start;
            return false;
        }

        public override void Write(IWriter writer)
        {
            using (writer.Indent("and"))
            {
                Expression.GetMatcher().Write(writer);
            }
        }
    }
}
