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
        public AnyRule Rule { get; internal set; }

        protected override IMatcher MakeMatcher()
        {
            Debug.Assert(Rule != null);
            var matcher = Rule == Grammar.Space
                ? new MatchSpace(Rule.Name, Grammar.CachedMatch[Rule.Name], Grammar.CachedMatches[Rule.Name])
                : new MatchName(Rule.Name, Grammar.CachedMatch[Rule.Name], Grammar.CachedMatches[Rule.Name]);

            Grammar.ReferencesToResolve.Add(matcher);
            return matcher;
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
