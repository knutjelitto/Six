namespace SixComp
{
    public class Lexer
    {
        private readonly Source source;

        public Lexer(Source source)
        {
            this.source = source;

            start = 0;
            current = 0;
        }

        private int start;
        private int current;

        public bool Done => current == source.Lenght;

        public string Rest => source.Chars(new Span(source, current, source.Lenght));

        public Token? Next()
        {
            while (current < source.Lenght && char.IsWhiteSpace(source[current]))
            {
                current += 1;
            }

            start = current;

            if (current >= source.Lenght)
            {
                return null;
            }

            switch (source[current])
            {
                case '.':
                    current += 1;
                    return new Token(Span(), ToKind.Dot);
                case ';':
                    current += 1;
                    return new Token(Span(), ToKind.Semi);
                case ':':
                    current += 1;
                    return new Token(Span(), ToKind.Colon);

                case '(':
                    current += 1;
                    return new Token(Span(), ToKind.LParen);
                case ')':
                    current += 1;
                    return new Token(Span(), ToKind.RParent);
                case '{':
                    current += 1;
                    return new Token(Span(), ToKind.LBrace);
                case '}':
                    current += 1;
                    return new Token(Span(), ToKind.RBrace);
                case '[':
                    current += 1;
                    return new Token(Span(), ToKind.LBrack);
                case ']':
                    current += 1;
                    return new Token(Span(), ToKind.RBrack);

                case '+':
                    current += 1;
                    return new Token(Span(), ToKind.OpPlus);
                case '-':
                    current += 1;
                    return new Token(Span(), ToKind.OpMinus);
                case '*':
                    current += 1;
                    return new Token(Span(), ToKind.OpMul);
                case '/':
                    current += 1;
                    return new Token(Span(), ToKind.OpDiv);
                default:
                    if (char.IsLetter(source[current]))
                    {
                        do
                        {
                            current += 1;
                        }
                        while (current < source.Lenght && char.IsLetterOrDigit(source[current]));

                        var span = Span();
                        var text = span.ToString();

                        return text switch
                        {
                            "let" => new Token(span, ToKind.KwLet),
                            "var" => new Token(span, ToKind.KwVar),
                            "func" => new Token(span, ToKind.KwFunc),
                            "return" => new Token(span, ToKind.KwReturn),
                            _ => new Token(span, ToKind.Identifier),
                        };
                    }
                    if (char.IsDigit(source[current]))
                    {
                        do
                        {
                            current += 1;
                        }
                        while (current < source.Lenght && char.IsDigit(source[current]));

                        var span = Span();

                        return new Token(span, ToKind.Number);
                    }
                    break;
            }

            return null;
        }

        private Span Span()
        {
            return new Span(source, start, current);
        }
    }
}
