using SixPeg.Matchers;

namespace SixPeg.Expression
{
    public class RuleExpression : AnyExpression
    {
        private IMatcher matcher = null;

        public RuleExpression(Identifier name, AnyExpression expression)
        {
            Name = name;
            Expression = expression;
            Used = false;
        }

        public Identifier Name { get; }
        public AnyExpression Expression { get; }
        public bool Used { get; set; }
        public bool Reached => matcher != null;

        public override IMatcher GetMatcher()
        {
            return matcher ??= new MatchRule(Name, Expression);
        }

        public override void Resolve(GrammarExpression grammar)
        {
            Expression.Resolve(grammar);
        }

        public override string ToString()
        {
            return Name.Text;
        }
    }
}
