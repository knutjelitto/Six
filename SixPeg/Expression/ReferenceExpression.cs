using SixPeg.Matchers;
using System.Diagnostics;

namespace SixPeg.Expression
{
    public class ReferenceExpression : AnyExpression
    {
        public ReferenceExpression(Symbol name)
        {
            Name = name;
        }

        public Symbol Name { get; }
        public RuleExpression Rule { get; internal set; }

        protected override IMatcher MakeMatcher()
        {
            Debug.Assert(Rule != null);
            return Rule == Grammar.Space
                ? new MatchSpace(Rule.Name, Grammar.Caches[Rule.Name], () => Rule.GetMatcher())
                : new MatchName(Rule.Name, Grammar.Caches[Rule.Name], () => Rule.GetMatcher());
        }

        public override string ToString()
        {
            return $"ref {Name}";
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
