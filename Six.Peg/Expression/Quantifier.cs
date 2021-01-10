namespace SixPeg.Expression
{
    public class Quantifier
    {
        public Quantifier(int min, int? max = null)
        {
            Min = min;
            Max = max;
        }

        public int Min { get; }

        public int? Max { get; }

    }
}
