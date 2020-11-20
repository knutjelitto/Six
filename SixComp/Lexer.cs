using SixComp.Support;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SixComp
{
    public class Lexer
    {
        public readonly Source Source;
        private readonly Dictionary<string, ToKind> keywords;

        public Lexer(Source source)
        {
            Source = source;

            Start = 0;
            current = 0;

            keywords = TokenHelper.GetKeywords().ToDictionary(kw => kw.rep, kw => kw.kind);
        }

        public int Start;
        private int current;

        public bool Done { get; private set; } = false;

        public char Current => current < Source.Length ? Source[current] : '\0';
        public char Next => current + 1 < Source.Length ? Source[current + 1] : '\0';
        public char NextNext => current + 2 < Source.Length ? Source[current + 2] : '\0';
        public bool More => current < Source.Length;

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
                    newlineBefore = newlineBefore || Source[current] == '\n';
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

            Start = current;

            if (current == Source.Length)
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
                case '\\':
                    return Token(ToKind.Backslash);

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
                    if (Next == '=')
                    {
                        return Token(ToKind.EqualEqual, 2);
                    }
                    return Token(ToKind.Equal);
                case '!':
                    if (Next == '=')
                    {
                        return Token(ToKind.BangEqual, 2);
                    }
                    return Token(ToKind.Bang);
                case '?':
                    return Token(ToKind.Quest);
                case '^':
                    return Token(ToKind.Caret);
                case '~':
                    return Token(ToKind.Tilde);
                case '&':
                    switch (Next)
                    {
                        case '&':
                            return Token(ToKind.AmperAmper, 2);
                        case '+':
                            return Token(ToKind.AmperPlus, 2);
                        case '-':
                            return Token(ToKind.AmperMinus, 2);
                        case '*':
                            return Token(ToKind.AmperAsterisk, 2);
                        case '/':
                            return Token(ToKind.AmperSlash, 2);
                        case '%':
                            return Token(ToKind.AmperPercent, 2);
                    }
                    return Token(ToKind.Amper);
                case '|':
                    if (Next == '|')
                    {
                        return Token(ToKind.VBarVBar, 2);
                    }
                    return Token(ToKind.VBar);
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
                    if (Next == '=')
                    {
                        return Token(ToKind.PlusEqual, 2);
                    }
                    return Token(ToKind.Plus);
                case '-':
                    if (Next == '>')
                    {
                        return Token(ToKind.MinusGreater, 2);
                    }
                    else if (Next == '=')
                    {
                        return Token(ToKind.MinusEqual, 2);
                    }
                    return Token(ToKind.Minus);
                case '*':
                    if (Next == '=')
                    {
                        return Token(ToKind.AsteriskEqual, 2);
                    }
                    return Token(ToKind.Asterisk);
                case '/':
                    if (Next == '=')
                    {
                        return Token(ToKind.SlashEqual, 2);
                    }
                    return Token(ToKind.Slash);
                case '%':
                    if (Next == '=')
                    {
                        return Token(ToKind.PercentEqual, 2);
                    }
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
                case '`':
                    {
                        do
                        {
                            current += 1;
                        }
                        while (char.IsLetter(Current));

                        if (Current == '`')
                        {
                            current += 1;

                            var text = Span().ToString();
                            var kw = text.Substring(1, text.Length - 2);

                            if (keywords.ContainsKey(kw))
                            {
                                return Token(ToKind.Name, 0);
                            }
                        }
                        break;
                    }
                case '$':
                    {
                        do
                        {
                            current += 1;
                        }
                        while (char.IsDigit(Current));

                        var text = Span().ToString();
                        if (text.Length > 1)
                        {
                            return Token(ToKind.Name, 0);
                        }

                        break;
                    }
                default:
                    if (char.IsLetter(Current) || Current == '_' || Current == '#')
                    {
                        do
                        {
                            current += 1;
                        }
                        while (char.IsLetterOrDigit(Current) || Current == '_' || Current == '²');

                        var text = Span().ToString();

                        if (keywords.TryGetValue(text, out var kind))
                        {
                            return Token(kind, 0);
                        }

                        if (text[0] == '#')
                        {
                            break; // -> error
                        }
                        return Token(ToKind.Name, 0);
                    }
                    if (char.IsDigit(Current))
                    {
                        if (Current == '0' && (Next == 'x' || Next == 'X'))
                        {
                            // HEX
                            current += 2;

                            int digits = 0;

                            while (IsHexDigit() || Current == '_')
                            {
                                digits += Current == '_' ? 0 : 1;
                                current += 1;
                            }

                            if (digits == 0)
                            {
                                throw new LexerException(Start, $"no digits in hexadecimal literal");
                            }

                            return Token(ToKind.Number, 0);

                        }
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

        private bool IsHexDigit()
        {
            var digit = Current;

            return char.IsDigit(digit) || (digit >= 'a' && digit <= 'f') || (digit >= 'A' && digit <= 'F');
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
            current = Math.Min(Source.Length, current + consume);
            return new Token(Span(), kind, newlineBefore);
        }

        private Span Span()
        {
            return new Span(Source, Start, current);
        }
    }
}
