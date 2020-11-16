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

        public char Current => current < source.Lenght ? source[current] : '\0';
        public char Next => current + 1 < source.Lenght ? source[current + 1] : '\0';
        public char NextNext => current + 2 < source.Lenght ? source[current + 2] : '\0';

        private bool newlineBefore;

        public Token GetNext()
        {
            if (Done)
            {
                return Token(ToKind.ERROR);
            }

            newlineBefore = false;
            while (char.IsWhiteSpace(Current))
            {
                newlineBefore = newlineBefore || source[current] == '\n';
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
                    if (Next == '.')
                    {
                        if (NextNext == '.')
                        {
                            return Token(ToKind.DotDotDot, 3);
                        }
                        else if (NextNext == '<')
                        {
                            return Token(ToKind.DotDotLess, 3);
                        }
                    }
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
                    return Token(ToKind.LBracket);
                case ']':
                    return Token(ToKind.RBracket);

                case '=':
                    return Token(ToKind.Equal);
                case '!':
                    return Token(ToKind.Bang);
                case '?':
                    return Token(ToKind.Quest);
                case '^':
                    return Token(ToKind.Caret);
                case '~':
                    return Token(ToKind.Tilde);

                case '<':
                    if (Next == '=')
                    {
                        return Token(ToKind.LessEqual, 2);
                    }
                    return Token(ToKind.Less);
                case '>':
                    if (Next == '=')
                    {
                        return Token(ToKind.GreaterEqual, 2);
                    }
                    return Token(ToKind.Greater);
                case '+':
                    return Token(ToKind.Plus);
                case '-':
                    if (Next == '>')
                    {
                        return Token(ToKind.MinusGreater, 2);
                    }
                    return Token(ToKind.Minus);
                case '*':
                    return Token(ToKind.Asterisk);
                case '/':
                    return Token(ToKind.Slash);
                case '%':
                    return Token(ToKind.Percent);
                case '"':
                    {
                        do
                        {
                            current += 1;
                        }
                        while (Current != 0 && Current != '\n' && Current != '\r' && Current != '"');

                        if (Current == '"')
                        {
                            current += 1;
                        }
                        return Token(ToKind.String, 0);
                    }
                default:
                    if (char.IsLetter(Current) || Current == '_')
                    {
                        do
                        {
                            current += 1;
                        }
                        while (char.IsLetterOrDigit(Current) || Current == '_');

                        var text = Span().ToString();

                        return text switch
                        {
                            "case" => Token(ToKind.KwCase, 0),
                            "class" => Token(ToKind.KwClass, 0),
                            "else" => Token(ToKind.KwElse, 0),
                            "enum" => Token(ToKind.KwEnum, 0),
                            "func" => Token(ToKind.KwFunc, 0),
                            "if" => Token(ToKind.KwIf, 0),
                            "init" => Token(ToKind.KwInit, 0),
                            "let" => Token(ToKind.KwLet, 0),
                            "protocol" => Token(ToKind.KwProtocol, 0),
                            "return" => Token(ToKind.KwReturn, 0),
                            "self" => Token(ToKind.KwSelf, 0),
                            "struct" => Token(ToKind.KwStruct, 0),
                            "var" => Token(ToKind.KwVar, 0),
                            _ => Token(ToKind.Name, 0),
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
            return new Token(Span(), kind, newlineBefore);
        }

        private Span Span()
        {
            return new Span(source, start, current);
        }
    }
}
