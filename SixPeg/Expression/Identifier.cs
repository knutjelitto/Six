namespace SixPeg.Expression
{
    public class Identifier
    {
        public Identifier(string text)
        {
            Text = text;
        }

        public string Text { get; }

        public override bool Equals(object obj)
        {
            return obj is Identifier other && other.Text == Text;
        }

        public override int GetHashCode()
        {
            return Text.GetHashCode();
        }

        public override string ToString() => Text;
    }
}
