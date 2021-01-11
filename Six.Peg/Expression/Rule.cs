using SixPeg.Visiting;
using System.Diagnostics;

namespace Six.Peg.Expression
{
    public class Rule : AnyExpression
    {
        public Rule(Symbol name, Attributes attributes, AnyExpression expression, bool isTerminal)
        {
            Name = name;
            Attributes = attributes;
            Expression = expression;
            IsTerminal = isTerminal;
            Used = false;

            if (attributes.Symbols.Count > 0)
            {
                Debug.Assert(true);
            }

        }

        public Symbol Name { get; }
        public Attributes Attributes { get; }
        public AnyExpression Expression { get; }
        public bool IsTerminal { get; }
        public bool Used { get; set; }

        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            return Name.Text;
        }
    }
}
