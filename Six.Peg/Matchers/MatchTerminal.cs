using Six.Peg.Runtime;
using SixPeg.Matches;
using SixPeg.Visiting;
using System;
using System.Collections.Generic;

namespace SixPeg.Matchers
{
    public class MatchTerminal : AnyMatcher
    {
        public MatchTerminal(string text, bool notMore)
        {
            Text = text;
            NotMore = notMore;
        }

        public override string Marker => Text;
        public override string DDLong => Text;

        public string Text { get; }
        public bool NotMore { get; }

        protected override bool InnerMatch(Context subject, ref int cursor)
        {
            throw new NotImplementedException();
        }

        protected override IMatch InnerMatch(Context subject, int before, int start)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<IMatch> InnerMatches(Context subject, int before, int start)
        {
            throw new NotImplementedException();
        }

        public override T Accept<T>(IMatcherVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
