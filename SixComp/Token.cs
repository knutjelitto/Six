namespace SixComp
{
    public struct Token
    {
        public Token(Span span, ToKind kind, bool newlineBefore)
        {
            Span = span;
            Kind = kind;
            NewlineBefore = newlineBefore;
        }

        public Span Span { get; }
        public ToKind Kind { get; }
        public bool NewlineBefore { get; }

        public bool IsDollar => Kind == ToKind.Name && Span.IsDollar;

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
