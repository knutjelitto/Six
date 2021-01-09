using SixPeg.Matches;
using SixPeg.Runtime;
using SixPeg.Visiting;
using System.Collections.Generic;

namespace SixPeg.Matchers
{
    public class MatchReference : AnyMatcher
    {
        public MatchReference(MatchRule rule)
        {
            Rule = rule;
        }

        public MatchRule Rule { get; }

        public override string Marker => $"{Rule.Name}";

        protected override IEnumerable<IMatch> InnerMatches(Context subject, int before, int start)
        {
            return Rule.Matches(subject, start);
        }

        protected override bool InnerMatch(Context subject, ref int cursor)
        {
            return Rule.Match(subject, ref cursor);
        }

        protected override IMatch InnerMatch(Context subject, int before, int start)
        {
            var match = Rule.Match(subject, start);
            if (match != null)
            {
                return IMatch.Success(this, before, start, match);
            }
            return null;
        }

        public override string DDLong => $"{Rule.Name}";

        public override T Accept<T>(IMatcherVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
