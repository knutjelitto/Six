using SixPeg.Matchers;
using System.Diagnostics;

namespace SixPeg.Expression
{
    public class ReferenceExpression : AnyExpression
    {
        public ReferenceExpression(Identifier name)
        {
            Name = name;
        }

        public Identifier Name { get; }
        public RuleExpression Rule { get; private set; }

        protected override IMatcher MakeMatcher()
        {
            Debug.Assert(Rule != null);
            return new MatchName(Rule.Name, Rule.GetMatcher());
        }

        protected override void InnerResolve()
        {
            Rule = Grammar.FindRule(Name);
        }

        public override string ToString()
        {
            return $"ref {Name}";
        }
    }
}
