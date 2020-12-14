using Six.Support;

namespace SixComp
{
    public class Token
    {
        private string? text;

        public Token(Span span, int index, ToKind kind, bool newlineBefore, ToFlags flags, ToData? data = null)
        {
            Span = span;
            Index = index;
            Kind = kind;
            NewlineBefore = newlineBefore;
            Flags = flags;
            Data = data;
        }

        public Span Span { get; }
        public int Index { get; }
        public ToKind Kind { get; }
        public bool NewlineBefore { get; }
        public ToFlags Flags { get; set; }
        public ToData? Data { get; }
        public string Text => text ??= Span.ToString();

        public bool IsDollar => Kind == ToKind.Name && Span.IsDollar;
        public bool IsImmediateDot => !Span.HasSpacing && Span.FirstChar == '.';

        public bool IsOperator => (Flags & ToFlags.AnyOperator) != 0;
        public bool IsKeyword => (Flags & ToFlags.Keyword) != 0;
        public bool IsImplicit => (Flags & ToFlags.Implicit) != 0;
        public int Length => Span.Length;
        public char First => Span.FirstChar;

        public void Error(IWriter writer, string error) => Span.Context.Error.Report(writer, error, this);

        public override bool Equals(object? obj) => obj is Token other && other.Kind == Kind;

        public override int GetHashCode() => Kind.GetHashCode();

        public override string ToString() => $"{Span}";
    }
}
