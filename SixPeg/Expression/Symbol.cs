using Pegasus.Common;

namespace SixPeg.Expression
{
    public class Symbol : ILexical
    {
        public Symbol(string text)
        {
            Text = text;
        }

        public string Text { get; }
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
