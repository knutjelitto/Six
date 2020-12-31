using SixPeg.Matchers;
using SixPeg.Visiting;
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
        public AnyRule Rule { get; internal set; }

        protected override AnyMatcher MakeMatcher()
        {
            Debug.Assert(Rule != null);
            var matcher = Rule == Grammar.Space
                ? new MatchSpace(Rule.Name, Grammar.CachedMatch[Rule.Name], Grammar.CachedMatches[Rule.Name])
                : new MatchRef(Rule.Name, Grammar.CachedMatch[Rule.Name], Grammar.CachedMatches[Rule.Name]);

            Grammar.ReferencesToResolve.Add(matcher);
            return matcher;
        }

        public override string ToString()
        {
            return $"ref {Name}";
        }

        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
