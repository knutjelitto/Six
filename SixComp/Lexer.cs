using SixComp.Support;
using System;

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

        public string Rest => source.Chars(new Span(source, current, source.Length));

        public char Current => current < source.Length ? source[current] : '\0';
        public char Next => current + 1 < source.Length ? source[current + 1] : '\0';
        public char NextNext => current + 2 < source.Length ? source[current + 2] : '\0';
        public bool More => current < source.Length;

        private bool newlineBefore;

        public Token GetNext()
        {
            if (Done)
            {
                return Token(ToKind.ERROR);
            }

            newlineBefore = false;
            do
            {
                while (char.IsWhiteSpace(Current))
                {
                    newlineBefore = newlineBefore || source[current] == '\n';
                    current += 1;
                }
                if (Current == '/')
                {
                    if (Next == '/')
                    {
                        SkipLineComment();
                    }
                    else if (Next == '*')
                    {
                        SkipMultilineComment();
                    }
                }
            }
            while (char.IsWhiteSpace(Current));

            start = current;

            if (current == source.Length)
            {
                Done = true;
                return Token(ToKind.EOF);
            }


            switch (Current)
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
                case '@':
                    return Token(ToKind.At);

                case '(':
                    return Token(ToKind.LParent);
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
                            "Any" => Token(ToKind.KwANY, 0),
                            "case" => Token(ToKind.KwCase, 0),
                            "class" => Token(ToKind.KwClass, 0),
                            "default" => Token(ToKind.KwDefault, 0),
                            "else" => Token(ToKind.KwElse, 0),
                            "enum" => Token(ToKind.KwEnum, 0),
                            "extension" => Token(ToKind.KwExtension, 0),
                            "false" => Token(ToKind.KwFalse, 0),
                            "func" => Token(ToKind.KwFunc, 0),
                            "get" => Token(ToKind.KwGet, 0),
                            "if" => Token(ToKind.KwIf, 0),
                            "import" => Token(ToKind.KwImport, 0),
                            "init" => Token(ToKind.KwInit, 0),
                            "inout" => Token(ToKind.KwInout, 0),
                            "let" => Token(ToKind.KwLet, 0),
                            "nil" => Token(ToKind.KwNil, 0),
                            "nonmutating" => Token(ToKind.KwNonmutating, 0),
                            "mutating" => Token(ToKind.KwMutating, 0),
                            "protocol" => Token(ToKind.KwProtocol, 0),
                            "public" => Token(ToKind.KwPublic, 0),
                            "return" => Token(ToKind.KwReturn, 0),
                            "self" => Token(ToKind.KwSelf, 0),
                            "Self" => Token(ToKind.KwSELF, 0),
                            "set" => Token(ToKind.KwSet, 0),
                            "struct" => Token(ToKind.KwStruct, 0),
                            "super" => Token(ToKind.KwSuper, 0),
                            "switch" => Token(ToKind.KwSwitch, 0),
                            "true" => Token(ToKind.KwTrue, 0),
                            "typealias" => Token(ToKind.KwTypealias, 0),
                            "var" => Token(ToKind.KwVar, 0),
                            "where" => Token(ToKind.KwWhere, 0),
                            _ => Token(ToKind.Name, 0),
                        };
                    }
                    if (char.IsDigit(source[current]))
                    {
                        do
                        {
                            current += 1;
                        }
                        while (char.IsDigit(Current) || Current == '_');

                        return Token(ToKind.Number, 0);
                    }
                    break;
            }

            return Token(ToKind.ERROR);
        }

        private void SkipLineComment()
        {
            current += 2;
            while (More && Current != '\n')
            {
                current += 1;
            }
        }

        private void SkipMultilineComment()
        {
            current += 2;
            while (More)
            {
                while (More && Current != '*')
                {
                    current += 1;
                }
                while (Current == '*')
                {
                    current += 1;
                }
                if (Current == '/')
                {
                    current += 1;
                    break;
                }
            }
        }

        private Token Token(ToKind kind, int consume = 1)
        {
            current = Math.Min(source.Length, current + consume);
            return new Token(Span(), kind, newlineBefore);
        }

        private Span Span()
        {
            return new Span(source, start, current);
        }
    }
}
