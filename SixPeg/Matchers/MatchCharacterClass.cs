using Six.Support;
using SixPeg.Expression;
using System.Linq;

namespace SixPeg.Matchers
{
    public class MatchCharacterClass : AnyMatchers
    {
        public MatchCharacterClass(ClassExpression expression)
            : base(expression.Ranges)
        {
            Expression = expression;
        }

        public ClassExpression Expression { get; }

        public override bool Match(string subject, ref int cursor)
        {
            var start = cursor;
            foreach (var matcher in Matchers)
            {
                if (matcher.Match(subject, ref cursor))
                {
                    return true;
                }
            }
            cursor = start;
            return false;
        }

        public override void Write(IWriter writer)
        {
            using (writer.Indent("ccharacter-class"))
            {
                foreach (var matcher in Matchers)
                {
                    matcher.Write(writer);
                }
            }
        }
    }
}
