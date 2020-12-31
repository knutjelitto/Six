namespace Six.Support
{
    public interface IVisitor<T>
    {
        T Visit(IVisitable visitable);
    }
}
