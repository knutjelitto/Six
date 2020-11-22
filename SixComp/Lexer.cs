using SixComp.Support;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SixComp
{
    public class Lexer
    {
        public readonly Source Source;
        private readonly Dictionary<string, ToKind> keywordMap;
        private readonly HashSet<ToKind> keywordSet;
        private readonly HashSet<ToKind> operatorSet;

        public Lexer(Source source)
        {
            Source = source;

            Start = 0;
            Before = 0;
            Index = 0;

            keywordMap = TokenHelper.GetKeywords().ToDictionary(kw => kw.rep, kw => kw.kind);
            keywordSet = new HashSet<ToKind>(keywordMap.Values);
            operatorSet = new HashSet<ToKind>(TokenHelper.GetOperators());
        }

        public int Start;
        public int Before;
        public int Index;

        public bool Done { get; private set; } = false;

        public char Current => Index < Source.Length ? Source[Index] : '\0';
        public char Next => Index + 1 < Source.Length ? Source[Index + 1] : '\0';
        public char NextNext => Index + 2 < Source.Length ? Source[Index + 2] : '\0';
        public bool More => Index < Source.Length;

        private bool newlineBefore;

        public Token GetNext()
        {
            if (Done)
            {
                return Token(ToKind.ERROR);
            }

            Before = Index;

            newlineBefore = false;
            do
            {
                while (char.IsWhiteSpace(Current))
                {
                    newlineBefore = newlineBefore || Source[Index] == '\n';
                    Index += 1;
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

            Start = Index;

            if (Index == Source.Length)
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
                            Index += 1;
                        }
                        while (Current != 0 && Current != '\n' && Current != '\r' && Current != '"');

                        if (Current == '"')
                        {
                            Index += 1;
                        }
                        return Token(ToKind.String, 0);
                    }
                case '`':
                    {
                        do
                        {
                            Index += 1;
                        }
                        while (char.IsLetter(Current));

                        if (Current == '`')
                        {
                            Index += 1;

                            var text = Text();
                            var kw = text.Substring(1, text.Length - 2);

                            if (keywordMap.ContainsKey(kw))
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
                            Index += 1;
                        }
                        while (char.IsDigit(Current));

                        var text = Text();
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
                            Index += 1;
                        }
                        while (char.IsLetterOrDigit(Current) || Current == '_' || Current == '²');

                        var text = Text();

                        if (keywordMap.TryGetValue(text, out var kind))
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
                        return LexNumber();
                    }
                    break;
            }

            return Token(ToKind.ERROR);
        }

        public bool IsKeyword(ToKind kind)
        {
            return keywordSet.Contains(kind);
        }

        public bool IsOperator(ToKind kind)
        {
            return operatorSet.Contains(kind);
        }

        private bool IsHexDigit(char digit)
        {
            return char.IsDigit(digit) || (digit >= 'a' && digit <= 'f') || (digit >= 'A' && digit <= 'F');
        }

        private bool IsBinDigit(char digit)
        {
            return digit == '0' || digit == '1';
        }

        private bool IsDecDigit(char digit)
        {
            return '0' <= digit && digit <= '9';
        }

        private Token LexNumber()
        {
            if (Current == '0')
            {
                if (Next == 'x')
                {
                    Index += 2;

                    LexHex();

                    if (Current == '.' && IsHexDigit(Next))
                    {
                        Index += 1;

                        LexHex();
                    }

                    if (Current == 'p' || Current == 'P')
                    {
                        Index += 1;

                        if (Current == '+' || Current == '-')
                        {
                            Index += 1;
                        }

                        LexDec();
                    }

                    return Token(ToKind.Number, 0);
                }
                else if (Next == 'b')
                {
                    Index += 2;

                    LexBin();

                    return Token(ToKind.Number, 0);
                }
            }

            LexDec();

            if (Current == '.' && IsDecDigit(Next))
            {
                Index += 1;

                LexDec();
            }

            if (Current == 'e' || Current == 'E')
            {
                Index += 1;

                if (Current == '+' || Current == '-')
                {
                    Index += 1;
                }

                LexDec();
            }

            return Token(ToKind.Number, 0);

            void LexHex()
            {
                if (IsHexDigit(Current))
                {
                    do
                    {
                        Index += 1;
                    }
                    while (IsHexDigit(Current) || Current == '_');

                }
                else
                {
                    throw new LexerException(Start, $"illformed hexadecimal literal");
                }
            }

            void LexBin()
            {
                if (IsBinDigit(Current))
                {
                    do
                    {
                        Index += 1;
                    }
                    while (IsBinDigit(Current) || Current == '_');
                }
                else
                {
                    throw new LexerException(Start, $"illformed binary literal");
                }
            }

            void LexDec()
            {
                if (IsDecDigit(Current))
                {
                    do
                    {
                        Index += 1;
                    }
                    while (IsDecDigit(Current) || Current == '_');
                }
                else
                {
                    throw new LexerException(Start, $"illformed decimal literal");
                }
            }

        }

        private void SkipLineComment()
        {
            Index += 2;
            while (More && Current != '\n')
            {
                Index += 1;
            }
        }

        private void SkipMultilineComment()
        {
            Index += 2;
            while (More)
            {
                while (More && Current != '*')
                {
                    Index += 1;
                }
                while (Current == '*')
                {
                    Index += 1;
                }
                if (Current == '/')
                {
                    Index += 1;
                    break;
                }
            }
        }

        private Token Token(ToKind kind, int consume = 1)
        {
            Index = Math.Min(Source.Length, Index + consume);
            var span = new Span(Source, Before, Start, Index);
            return new Token(span, kind, newlineBefore);
        }

        private string Text()
        {
            return Source.Content.Substring(Start, Index - Start);
        }
    }
}
