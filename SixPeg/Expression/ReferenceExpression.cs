using SixPeg.Visiting;

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
