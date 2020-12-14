using SixPeg.Matchers;

namespace SixPeg.Expression
{
    public class RuleExpression : AnyExpression
    {
        public RuleExpression(Identifier name, AnyExpression expression)
        {
            Name = name;
            Expression = expression;
            Used = false;
        }

        public Identifier Name { get; }
        public AnyExpression Expression { get; }
        public bool Used { get; set; }

        public override IMatcher GetMatcher(bool spaced)
        {
            return Expression.GetMatcher(spaced);
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
