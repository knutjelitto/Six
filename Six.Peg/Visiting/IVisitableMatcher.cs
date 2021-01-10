namespace SixPeg.Visiting
{
    public interface IVisitableMatcher
    {
        T Accept<T>(IMatcherVisitor<T> visitor);
    }
}
