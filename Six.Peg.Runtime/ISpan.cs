namespace Six.Peg.Runtime
{
    public interface ISpan
    {
        int Before { get; }
        int Start { get; }
        int End { get; }
        public int Length => End - Start;
        public bool HasSpacing => Before < Start;

    }
}
