using Pegasus.Common;
using System.Diagnostics;

namespace SixPeg.Expression
{
    public class Symbol : ILexical
    {
        public Symbol(string text, bool quoted = false)
        {
            Text = text;
            Quoted = quoted;

            if (quoted)
            {
                Text = $"'{Text}'";
                Debug.Assert(true);
            }
        }

        public string Text { get; }
        public bool Quoted { get; }
        public Cursor EndCursor { get; set; }
        public Cursor StartCursor { get; set; }

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
