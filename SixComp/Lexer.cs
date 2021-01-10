using Six.Peg.Runtime;
using SixComp.Support;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SixComp
{
    [DebuggerDisplay("{Status()}")]
    public class Lexer
    {
        private readonly Dictionary<string, ToKind> keywordMap;
        private readonly HashSet<ToKind> keywordSet;
        private readonly HashSet<ToKind> operatorSet;
        private readonly Queue<Token> greaters;

        public Lexer(Context context)
        {
            Context = context;

            Start = 0;
            Before = 0;
            Index = 0;

            keywordMap = TokenHelper.GetKeywords().ToDictionary(kw => kw.rep, kw => kw.kind);
            keywordSet = new HashSet<ToKind>(keywordMap.Values);
            operatorSet = new HashSet<ToKind>(TokenHelper.GetOperators());
            greaters = new Queue<Token>();
        }

        public int Before;
        public int Start;
        public int Index;

        private int Length => Index - Start;

        public Context Context { get; }
        public Source Source => Context.Source;
        public string Text => Source.Text;
        public Tokens Tokens => Context.Tokens;

        public bool Done { get; private set; } = false;

        public char Current => Source[Index];
        public char Next => Source[Index + 1];
        public char NextNext => Source[Index + 2];
        public bool More => Index < Source.Length;

        public bool IsKeyword(ToKind kind)
        {
            return keywordSet.Contains(kind);
        }

        public bool IsOperator(ToKind kind)
        {
            return operatorSet.Contains(kind);
        }

        private bool newlineBefore;

        public void BackupForSplit(Token token, ToKind kind)
        {
            Before = token.Span.Before;
            Start = token.Span.Start;
            Index = token.Span.Start + 1;

            var split = AnyOperator(kind, withIndex: token.Index);
            Tokens.BackupForSplit(split);
        }

        public string Status()
        {
            var lb = Math.Min(Index, 15);
            var la = Math.Max(Index - Source.Length, 15);

            return Source.Text.Substring(Index - lb, lb) + "•" + Source.Text.Substring(Index, la);
        }

        public Token GetNext()
        {
            if (Done)
            {
                return Token(ToKind.ERROR, 0);
            }

            Before = Index;

            LexWhitespace();

            Start = Index;

            if (Index == Source.Length)
            {
                Done = true;
                return Token(ToKind.EOF, 0);
            }

            switch (Current)
            {
                case ';':
                    return Token(ToKind.SemiColon, 1, ToFlags.OpSpaceAny);
                case ':':
                    return Token(ToKind.Colon, 1, ToFlags.OpSpaceAny);
                case ',':
                    return Token(ToKind.Comma, 1, ToFlags.OpSpaceAny);

                case '@':
                    return Token(ToKind.At, 1);
                case '\\':
                    return Token(ToKind.Backslash, 1);

                case '(':
                    return Token(ToKind.LParent, 1, ToFlags.OpSpaceBefore);
                case ')':
                    return Token(ToKind.RParent, 1, ToFlags.OpSpaceAfter);
                case '{':
                    return Token(ToKind.LBrace, 1, ToFlags.OpSpaceBefore);
                case '}':
                    return Token(ToKind.RBrace, 1, ToFlags.OpSpaceAfter);
                case '[':
                    return Token(ToKind.LBracket, 1, ToFlags.OpSpaceBefore);
                case ']':
                    return Token(ToKind.RBracket, 1, ToFlags.OpSpaceAfter);

                case '"':
                    return LexStringLiteral(0);
                case '`':
                    return LexQuotedIdentifier();
                case '$':
                    {
                        if (char.IsDigit(Next))
                        {
                            Index += 1;
                            do
                            {
                                Index += 1;
                            }
                            while (char.IsDigit(Current));

                            var text = CurrentText();
                            if (text.Length > 1)
                            {
                                return Token(ToKind.Name, 0, ToFlags.Implicit);
                            }
                        }

                        break;
                    }
                default:
                    var hash = 0;
                    if (Current == '#')
                    {
                        Debug.Assert(true);
                    }
                    while (Index + hash < Source.Length && Source[Index + hash] == '#')
                    {
                        hash += 1;
                    }
                    if (hash > 0 && Source[Index + hash] == '"' )
                    {
                        return LexStringLiteral(hash);
                    }
                    if (char.IsLetter(Current) || Current == '_' || Current == '#')
                    {
                        do
                        {
                            Index += 1;
                        }
                        while (char.IsLetterOrDigit(Current) || Current == '_' || Current == '²');

                        var text = CurrentText();

                        if (text == "self")
                        {
                            Debug.Assert(true);
                        }

                        if (keywordMap.TryGetValue(text, out var kind))
                        {
                            return Token(kind, 0, ToFlags.Keyword);
                        }

                        if (text[0] == '#')
                        {
                            break; // -> error
                        }
                        return Token(ToKind.Name, 0);
                    }
                    if (char.IsDigit(Current))
                    {
                        return LexNumberLiteral();
                    }
                    if (DotOperatorHead(Current))
                    {
                        Index += 1;
                        while (DotOperatorCharacter(Current))
                        {
                            Index += 1;
                        }
                        if (Length == 1)
                        {
                            return AnyOperator(ToKind.Dot);
                        }
                        if (Length == 3 && Text[Start + 1] == '.' && Text[Start + 2] == '.')
                        {
                            return AnyOperator(ToKind.DotDotDot);
                        }
                        return AnyOperator();
                    }
                    if (OperatorHead(Current))
                    {
                        Index += 1;
                        while (OperatorCharacter(Current))
                        {
                            Index += 1;
                        }
                        if (Length == 1)
                        {
                            switch (Text[Start])
                            {
                                case '?':
                                    return AnyOperator(ToKind.Quest);
                                case '!':
                                    return AnyOperator(ToKind.Bang);
                                case '=':
                                    return AnyOperator(ToKind.Assign);
                                case '<':
                                    return AnyOperator(ToKind.Less);
                                case '>':
                                    return AnyOperator(ToKind.Greater);
                                case '&':
                                    return AnyOperator(ToKind.Amper);
                            }
                        }
                        else if (Length == 2)
                        {
                            if (Text[Start] == '-' && Text[Start+1] == '>')
                            {
                                return AnyOperator(ToKind.Arrow);
                            }
                            if (Text[Start] == '=' && Text[Start + 1] == '=')
                            {
                                return AnyOperator(ToKind.Equals);
                            }
                            if (Text[Start] == '?' && Text[Start + 1] == '>')
                            {
                                Index -= 1; // backup one char
                                return AnyOperator(ToKind.Quest);
                            }
                        }

                        var split = (Text[Start]) switch
                        {
                            '<' => ToFlags.OpSplitLess,
                            '>' => ToFlags.OpSplitGreater,
                            '?' => ToFlags.OpSplitQuest,
                            '!' => ToFlags.OpSplitBang,
                            _ => ToFlags.None,
                        };
                        return AnyOperator(flags: split);
                    }
                    break;
            }

            return Token(ToKind.ERROR, 0);
        }

        private void LexIdentifier()
        {
            // Current is IdentifierHead

            Index += 1;
            while (IdentifierCharacter())
            {
                Index += 1;
            }
        }

        private Token LexQuotedIdentifier()
        {
            // Current is '`'

            Index += 1;
            if (IdentifierHead())
            {
                LexIdentifier();

                if (Current == '`')
                {
                    Index += 1;

                    var text = CurrentText();
                    var kw = text[1..^1];

                    if (keywordMap.ContainsKey(kw))
                    {
                        return Token(ToKind.Name, 0);
                    }

                    throw new LexerException(Start, $"quoted keyword isn't really a keyword");
                }
            }

            throw new LexerException(Start, $"illformed quoted identifier/keyword");
        }

        private bool IdentifierHead()
        {
            return 'a' <= Current && Current <= 'z' || 'A' <= Current && Current <= 'Z' || Current == '_';
        }

        private bool IdentifierCharacter()
        {
            return IdentifierHead()
                || IsDecDigit(Current)
                || Current == '²';
        }

        private void LexWhitespace()
        {
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
        }

        public Token LexStringLiteral(int hash)
        {
            Index += hash;

            Debug.Assert(Current == '"');

            // Current == '"'

            if (Next == '"' && NextNext == '"')
            {
                // long dong silver
                Index += 3;
                while (Current != 0 && (Current != '"' || Next != '"' || NextNext != '"'))
                {
                    Index += 1;
                }
                return Token(ToKind.String, 3);
            }
            else
            {
                if (hash > 0)
                {
                    return LexHashString(hash);
                }

                do
                {
                    if (Current == '\\' && Next == '"')
                    {
                        Index += 2;
                    }
                    else if (Current == '\\' && Next == '\\')
                    {
                        Index += 2;
                    }
                    else
                    {
                        Index += 1;
                    }
                }
                while (Current != 0 && Current != '\n' && Current != '\r' && Current != '"');

                if (Current == '"')
                {
                    Index += 1;
                }
            }
            return Token(ToKind.String, 0);
        }

        private Token LexHashString(int hash)
        {
            Debug.Assert(Current == '"');
            Debug.Assert(hash > 0);

            while (true)
            {
                Index += 1;
                if (Current == '\\' && Next == '#')
                {
                    do
                    {
                        Index += 1;
                    }
                    while (Current == '#');
                }
                if (Current == '"' && Next == '#')
                {
                    Index += 1;
                    var count = 0;
                    while (count < hash && Current == '#')
                    {
                        Index += 1;
                        count += 1;
                    }
                    if (count == hash)
                    {
                        return Token(ToKind.String, 0);
                    }
                }

                if (Index - Start > 100)
                {
                    return Token(ToKind.ERROR, 0);
                }
            }
        }

        public bool OperatorHead(char c)
        {
            switch(c)
            {
                case '/':
                case '=':
                case '-':
                case '+':
                case '!':
                case '*':
                case '%':
                case '<':
                case '>':
                case '&':
                case '|':
                case '^':
                case '~':
                case '?':
                    return true;
            }

            return false;
        }

        public bool OperatorCharacter(char c)
        {
            return OperatorHead(c);
        }

        public bool DotOperatorHead(char c)
        {
            return c == '.';
        }

        public bool DotOperatorCharacter(char c)
        {
            return c == '.' || OperatorCharacter(c);
        }

        private bool IsBinDigit(char digit)
        {
            return digit == '0' || digit == '1';
        }

        private bool IsOctDigit(char digit)
        {
            return '0' <= digit && digit <= '7';
        }

        private bool IsDecDigit(char digit)
        {
            return '0' <= digit && digit <= '9';
        }

        private bool IsHexDigit(char digit)
        {
            return char.IsDigit(digit) || (digit >= 'a' && digit <= 'f') || (digit >= 'A' && digit <= 'F');
        }

        private Token LexNumberLiteral()
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
                else if (Next == 'o')
                {
                    Index += 2;

                    LexOct();

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

            void LexOct()
            {
                if (IsOctDigit(Current))
                {
                    do
                    {
                        Index += 1;
                    }
                    while (IsOctDigit(Current) || Current == '_');
                }
                else
                {
                    throw new LexerException(Start, $"illformed octal literal");
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

        private Token Token(ToKind kind, int consume, ToFlags flags = ToFlags.None)
        {
            Index = Math.Min(Source.Length, Index + consume);
            var span = new Span(Context, Before, Start, Index);
            return new Token(span, Tokens.Count, kind, newlineBefore, flags);
        }

        private Token AnyOperator(ToKind kind = ToKind.Operator, ToFlags flags = ToFlags.None, int? withIndex = null)
        {
            Debug.Assert(Index > Start);
            var span = new Span(Context, Before, Start, Index);
            return new Token(span, withIndex ?? Tokens.Count, kind, newlineBefore, ToFlags.Operator | flags);
        }

        private string CurrentText()
        {
            return Source.Text[Start..Index];
        }
    }
}
