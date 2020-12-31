namespace SixPeg.Visiting
{
    public interface IVisitableExpression
    {
        T Accept<T>(IExpressionVisitor<T> visitor);
    }
}
