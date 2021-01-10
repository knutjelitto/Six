using SixPeg.Matchers;
using System.Linq;

namespace SixPeg.Visiting
{
    public class MatcherVisitor : IMatcherVisitor<bool>
    {
        public virtual bool Visit(MatchAnd matcher)
        {
            return matcher.Matcher.Accept(this);
        }

        public virtual bool Visit(MatchBefore matcher)
        {
            return matcher.Matcher.Accept(this);
        }

        public virtual bool Visit(MatchCharacterAny matcher)
        {
            return true;
        }

        public virtual bool Visit(MatchCharacterExact matcher)
        {
            return true;
        }

        public virtual bool Visit(MatchCharacterRange matcher)
        {
            return true;
        }

        public virtual bool Visit(MatchCharacterSequence matcher)
        {
            return true;
        }

        public virtual bool Visit(MatchCharacterSet matcher)
        {
            return true;
        }

        public virtual bool Visit(MatchChoice matcher)
        {
            return matcher.Matchers.All(m => m.Accept(this));
        }

        public virtual bool Visit(MatchEpsilon matcher)
        {
            return true;
        }

        public virtual bool Visit(MatchError matcher)
        {
            return true;
        }

        public virtual bool Visit(MatchReference matcher)
        {
            return matcher.Rule.Accept(this);
        }

        public virtual bool Visit(MatchRule matcher)
        {
            return matcher.Matcher.Accept(this);
        }

        public virtual bool Visit(MatchNot matcher)
        {
            return matcher.Matcher.Accept(this);
        }

        public virtual bool Visit(MatchOneOrMore matcher)
        {
            return matcher.Matcher.Accept(this);
        }

        public virtual bool Visit(MatchSequence matcher)
        {
            return matcher.Matchers.All(m => m.Accept(this));
        }

        public virtual bool Visit(MatchZeroOrMore matcher)
        {
            return matcher.Matcher.Accept(this);
        }

        public virtual bool Visit(MatchZeroOrOne matcher)
        {
            return matcher.Matcher.Accept(this);
        }
    }
}
