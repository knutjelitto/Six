using SixComp.Support;

namespace SixComp
{
    public struct Span
    {
        public readonly Source Source;
        private readonly int Before;
        public readonly int Start;
        public readonly int End;

        public Span(Source source, int before, int start, int end)
        {
            Source = source;
            Before = before;
            Start = start;
            End = end;
        }

        public int Length => End - Start;

        public bool IsDollar => End > Start && Source.Content[Start] == '$';

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
