namespace Six.Peg.Runtime
{
    public interface ISource
    {
        string Name { get; }
        string Text { get; }
        SourceIndex Index { get; }
    }
}
