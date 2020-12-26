using SixPeg.Matchers;

namespace SixPeg.Expression
{
    public abstract class AnyRule : AnyExpression
    {
        public AnyRule(Symbol name, AnyExpression expression, bool isTerminal)
        {
            Name = name;
            Expression = expression;
            IsTerminal = isTerminal;
            Used = false;
        }

        public Symbol Name { get; }
        public AnyExpression Expression { get; }
        public bool IsTerminal { get; }
        public bool Used { get; set; }

        protected override IMatcher MakeMatcher()
        {
            return Expression.GetMatcher();
        }

        public override string ToString()
        {
            return Name.Text;
        }
    }
}
