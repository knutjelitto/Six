namespace SixComp
{
    public struct Token
    {
        public Token(int index, ToFlags flags, Span span, ToKind kind, bool newlineBefore)
        {
            Index = index;
            Flags = flags;
            Span = span;
            Kind = kind;
            NewlineBefore = newlineBefore;
        }

        public int Index { get; }
        public ToFlags Flags { get; }
        public Span Span { get; }
        public ToKind Kind { get; }
        public bool NewlineBefore { get; }
        public string Text => Span.ToString();

        public bool IsDollar => Kind == ToKind.Name && Span.IsDollar;

        public bool IsOperator => (Flags & ToFlags.AnyOperator) != 0;

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
