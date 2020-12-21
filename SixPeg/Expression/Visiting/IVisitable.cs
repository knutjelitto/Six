namespace SixPeg.Expression
{
    public interface IVisitable
    {
        T Accept<T>(IVisitor<T> visitor);
    }
}
