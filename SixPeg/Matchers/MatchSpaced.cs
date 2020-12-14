using Six.Support;
using SixPeg.Expression;

namespace SixPeg.Matchers
{
    public class MatchSpaced : AnyMatcher
    {
        private IMatcher spacesMatcher;

        public MatchSpaced(RuleExpression spaces, AnyExpression expression)
        {
            Spaces = spaces;
            Expression = expression;
            Matcher = Expression.GetMatcher();
        }

        public RuleExpression Spaces { get; }
        public AnyExpression Expression { get; }

        public IMatcher SpacesMatcher => spacesMatcher ??= Spaces.GetMatcher();
        public IMatcher Matcher { get; private set; }

        public override bool Match(string subject, ref int cursor)
        {
            var start = cursor;
            if (SpacesMatcher.Match(subject, ref cursor))
            {
                if (Matcher.Match(subject, ref cursor))
                {
                    return true;
                }
            }
            cursor = start;
            return false;
        }

        public override void Write(IWriter writer)
        {
            using (writer.Indent($"spaced `{Spaces.Name.Text}`"))
            {
                Matcher.Write(writer);
            }
        }
    }
}
