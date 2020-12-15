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

        protected override IMatcher MakeMatcher()
        {
            return Expression.GetMatcher();
        }

        protected override void InnerResolve()
        {
            Expression.Resolve(Grammar);
        }

        public override string ToString()
        {
            return Name.Text;
        }
    }
}
