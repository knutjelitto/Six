namespace SixComp
{
    public class Token
    {
        public Token(int index, Span span, ToKind kind, bool newlineBefore, ToFlags flags, ToData? data = null)
        {
            Index = index;
            Span = span;
            Kind = kind;
            NewlineBefore = newlineBefore;
            Flags = flags;
            Data = data;
        }

        public int Index { get; }
        public Span Span { get; }
        public ToKind Kind { get; }
        public bool NewlineBefore { get; }
        public ToFlags Flags { get; set; }
        public ToData? Data { get; }
        public string Text => Span.ToString();

        public bool IsDollar => Kind == ToKind.Name && Span.IsDollar;

        public bool IsImmediateDot => !Span.Spacing && Span.First == '.';

        public bool IsOperator => (Flags & ToFlags.AnyOperator) != 0;
        public bool IsImplicit => (Flags & ToFlags.Implicit) != 0;
        public int Length => Span.Length;
        public char First => Span.First;

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
