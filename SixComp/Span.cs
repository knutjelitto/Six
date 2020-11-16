namespace SixComp
{
    public struct Span
    {
        public readonly Source Source;
        public readonly int Start;
        public readonly int End;

        public Span(Source source, int start, int end)
        {
            Source = source;
            Start = start;
            End = end;
        }

        public int Length => End - Start;

        public string GetLine()
        {
            return Source.LineFor(this);
        }

        public override string ToString()
        {
            return Source.Chars(this);
        }
    }
}
