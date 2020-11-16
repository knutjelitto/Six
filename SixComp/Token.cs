namespace SixComp
{
    public struct Token
    {
        public Token(Span span, ToKind kind, bool newLine)
        {
            Span = span;
            Kind = kind;
            NewLine = newLine;
        }

        public Span Span { get; }
        public ToKind Kind { get; }
        public bool NewLine { get; }

        public override bool Equals(object? obj)
        {
            return obj is Token other && other.Kind == Kind;
        }

        public override int GetHashCode()
        {
            return Kind.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Span}";
        }
    }
}
