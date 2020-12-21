using Six.Support;

namespace SixComp
{
    public struct Span : ISpan
    {
        public readonly Context Context;
        public int Before { get; }
        public int Start { get; }
        public int End { get; }

        public Span(Context context, int before, int start, int end)
        {
            Context = context;
            Before = before;
            Start = start;
            End = end;
        }

        public Source Source => Context.Source;
        public int Length => End - Start;
        public bool HasSpacing => Before < Start;
        public bool IsDollar => End > Start && Source.Text[Start] == '$';
        public char FirstChar => Source.Text[Start];

        public override string ToString()
        {
            return Source.Chars(this);
        }
    }
}
