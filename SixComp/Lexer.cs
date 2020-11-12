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

        public bool Done { get; private set; } = false;

        public string Rest => source.Chars(new Span(source, current, source.Lenght));


        public Token Next()
        {
            if (Done)
            {
                return Token(ToKind.ERROR);
            }

            while (current < source.Lenght && char.IsWhiteSpace(source[current]))
            {
                current += 1;
            }

            start = current;

            if (current == source.Lenght)
            {
                Done = true;
                return Token(ToKind.EOF);
            }

            switch (source[current])
            {
                case '.':
                    return Token(ToKind.Dot);
                case ';':
                    return Token(ToKind.Semi);
                case ':':
                    return Token(ToKind.Colon);
                case ',':
                    return Token(ToKind.Comma);

                case '(':
                    return Token(ToKind.LParen);
                case ')':
                    return Token(ToKind.RParent);
                case '{':
                    return Token(ToKind.LBrace);
                case '}':
                    return Token(ToKind.RBrace);
                case '[':
                    return Token(ToKind.LBrack);
                case ']':
                    return Token(ToKind.RBrack);

                case '=':
                    return Token(ToKind.Assign);
                case '!':
                    return Token(ToKind.Bang);
                case '?':
                    return Token(ToKind.Quest);
                case '^':
                    return Token(ToKind.Caret);
                case '~':
                    return Token(ToKind.Tilde);

                case '<':
                    return Token(ToKind.Less);
                case '>':
                    return Token(ToKind.Greater);
                case '+':
                    return Token(ToKind.Plus);
                case '-':
                    return Token(ToKind.Minus);
                case '*':
                    return Token(ToKind.Asterisk);
                case '/':
                    return Token(ToKind.Slash);
                case '%':
                    return Token(ToKind.Percent);
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
                            "case" => new Token(span, ToKind.KwCase),
                            "class" => new Token(span, ToKind.KwClass),
                            "struct" => new Token(span, ToKind.KwStruct),
                            "enum" => new Token(span, ToKind.KwEnum),
                            "return" => new Token(span, ToKind.KwReturn),
                            _ => new Token(span, ToKind.Name),
                        };
                    }
                    if (char.IsDigit(source[current]))
                    {
                        do
                        {
                            current += 1;
                        }
                        while (current < source.Lenght && char.IsDigit(source[current]));

                        return Token(ToKind.Number, 0);
                    }
                    break;
            }

            return Token(ToKind.ERROR);
        }

        private Token Token(ToKind kind, int consume = 1)
        {
            if (current < source.Lenght)
            {
                current += consume;
            }
            return new Token(Span(), kind);
        }

        private Span Span()
        {
            return new Span(source, start, current);
        }
    }
}
