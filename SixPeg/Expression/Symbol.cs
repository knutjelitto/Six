using Six.Support;
using System.Diagnostics;

namespace SixPeg.Expression
{
    public class Symbol
    {
        public Symbol(Source source, string text, int start, int next, bool quoted = false)
        {
            Source = source;
            Text = text;
            Start = start;
            Next = next;
            Quoted = quoted;

            if (quoted)
            {
                Text = $"'{Text}'";
                Debug.Assert(true);
            }
        }

        public Source Source { get; }
        public string Text { get; }
        public int Start { get; }
        public int Next { get; }
        public int Length => Next - Start;
        public bool Quoted { get; }

        public override bool Equals(object obj)
        {
            return obj is Symbol other && other.Text == Text;
        }

        public override int GetHashCode()
        {
            return Text.GetHashCode();
        }

        public override string ToString() => Text;
    }
}
