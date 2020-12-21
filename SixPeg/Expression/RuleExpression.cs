using SixPeg.Matchers;
using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Expression
{
    public class RuleExpression : AnyExpression
    {
        public RuleExpression(Symbol name, IEnumerable<Symbol> flags, AnyExpression expression)
        {
            Name = name;
            Flags = flags.ToArray();
            Expression = expression;
            Used = false;
        }

        public Symbol Name { get; }
        public IReadOnlyList<Symbol> Flags { get; }
        public AnyExpression Expression { get; }
        public bool Used { get; set; }

        protected override IMatcher MakeMatcher()
        {
            return Expression.GetMatcher();
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            return Name.Text;
        }
    }
}
