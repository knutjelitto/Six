using SixComp.Support;

namespace SixComp
{
    public struct Span
    {
        public readonly Context Context;
        public readonly int Before;
        public readonly int Start;
        public readonly int End;

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
        public bool IsDollar => End > Start && Source.Content[Start] == '$';
        public char FirstChar => Source.Content[Start];

        public override string ToString()
        {
            return Source.Chars(this);
        }
    }
}
