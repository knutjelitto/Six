namespace Six.Support
{
    public interface IVisitable
    {
        T Accept<T>(IVisitor<T> visitor);
    }
}
