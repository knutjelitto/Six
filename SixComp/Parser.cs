using SixComp.ParseTree;
using SixComp.Support;
using System;
using System.Diagnostics;

namespace SixComp
{
    [DebuggerDisplay("{CurrentToken}-{NextToken}-{NextNextToken}")]
    public class Parser
    {
        private readonly Lexer Lexer;
        private readonly Tokens Tokens;

        public int Offset;
        public int ClassifiedOffset;

        public Parser(Context context)
        {
            Context = context;
            Lexer = Context.Lexer;
            Tokens = Context.Tokens;
            Offset = 0;
            ClassifiedOffset = 0;
        }

        public Context Context { get; }

        public IDisposable InBacktrack()
        {
            var offset = Offset;

            return new Disposable(() => Offset = offset);
        }

        public Unit Parse()
        {
            return Unit.Parse(this);
        }

        public void SplitGreater()
        {

        }

        private void ClassifyOperator()
        {
            if (ClassifiedOffset < Offset)
            {
                ClassifiedOffset = Offset;

                var current = CurrentToken;
                if ((current.Flags & ToFlags.AnyOperator) == ToFlags.Operator)
                {
                    // unclassified

                    var prev = PrevToken;
                    var spaceBefore = current.Span.Spacing || (prev.Flags & ToFlags.OpSpaceBefore) != 0;
                    var next = NextToken;
                    var spaceAfter = next.Span.Spacing || (next.Flags & ToFlags.OpSpaceAfter) != 0;

                    if (!spaceBefore && (spaceAfter || next.IsImmediateDot || current.Kind == ToKind.Quest || current.Kind == ToKind.Bang))
                    {
                        current.Flags |= ToFlags.PostfixOperator;
                    }
                    else if (spaceBefore && !spaceAfter)
                    {
                        current.Flags |= ToFlags.PrefixOperator;
                    }
                    else // !spaceBefore && !spaceAfter || spaceBefore && spaceAfter
                    {
                        current.Flags |= ToFlags.InfixOperator;
                    }
                }
            }
        }

        public bool IsPrefixOperator()
        {
            ClassifyOperator();
            return (CurrentToken.Flags & ToFlags.PrefixOperator) != 0;
        }

        public bool IsPostfixOperator()
        {
            ClassifyOperator();
            return (CurrentToken.Flags & ToFlags.PostfixOperator) != 0;
        }

        public bool IsInfixOperator()
        {
            ClassifyOperator();
            return (CurrentToken.Flags & ToFlags.InfixOperator) != 0;
        }

        public bool IsKeyword => CurrentToken.IsKeyword;

        public T? Try<T>(ToKind start, Func<Parser, T> parse)
            where T : class
        {
            if (Current == start)
            {
                return parse(this);
            }
            return null;
        }

        public T? Try<T>(TokenSet start, Func<Parser, T> parse)
            where T : class
        {
            if (start.Contains(Current))
            {
                return parse(this);
            }
            return null;
        }

        public T? TryMatch<T>(ToKind start, Func<Parser, T> parse)
            where T : class
        {
            if (Match(start))
            {
                return parse(this);
            }
            return null;
        }

        public T TryList<T>(ToKind start, Func<Parser, T> parse)
            where T : class, new()
        {
            if (Current == start)
            {
                return parse(this);
            }
            return new T();
        }

        public T TryList<T>(TokenSet start, Func<Parser, T> parse)
            where T : class, new()
        {
            if (start.Contains(Current))
            {
                return parse(this);
            }
            return new T();
        }

        public ToKind Prev => Ahead(-1).Kind;
        public ToKind Current => Ahead(0).Kind;
        public ToKind Next => Ahead(1).Kind;
        public ToKind NextNext => Ahead(2).Kind;

        public Token PrevToken => Ahead(-1);
        public Token CurrentToken => Ahead(0);
        public Token NextToken => Ahead(1);
        public Token NextNextToken => Ahead(2);

        /// <summary>
        /// Check if current token represent an operator
        /// </summary>
        public bool IsOperator => CurrentToken.IsOperator;
        public bool IsImplicit => CurrentToken.IsImplicit;

        public bool Adjacent => Ahead(-1).Span.End == Ahead(0).Span.Start;

        public Token Ahead(int offset)
        {
            return At(Offset + offset);
        }

        public Token At(int offset)
        {
            return Tokens[offset];
        }

        public Token ConsumeAny()
        {
            var token = CurrentToken;
            Offset += 1;
            return token;
        }

        public Token? ConsumeCarefully(ToKind kind)
        {
            if (Current == kind)
            {
                return Consume(kind);
            }

            var token = CurrentToken;

            Debug.Assert(token.Index == Offset);

            switch (kind)
            {
                case ToKind.Less when (token.Flags & ToFlags.OpSplitLess) != 0:
                    Split(token, kind);
                    break;
                case ToKind.Greater when (token.Flags & ToFlags.OpSplitGreater) != 0:
                    Split(token, kind);
                    break;
                case ToKind.Quest when (token.Flags & ToFlags.OpSplitQuest) != 0:
                    Split(token, kind);
                    break;
                case ToKind.Bang when (token.Flags & ToFlags.OpSplitBang) != 0:
                    Split(token, kind);
                    break;
                default:
                    return null;
            }

            return Consume(kind);
        }

        private void Split(Token token, ToKind kind)
        {
            Lexer.BackupForSplit(token, kind);
        }

        public Token Consume(string text)
        {
            var token = CurrentToken;
            if (token.Text == text)
            {
                Offset += 1;
                return token;
            }

            throw new ParserException(token, $"expected `{text}`, but got `{token}`");
        }

        public Token Consume(ToKind kind)
        {
            var token = CurrentToken;
            if (token.Kind == kind)
            {
                Offset += 1;
                return token;
            }

            throw new ParserException(token, $"expected `{kind.GetRep()}`, but got `{token}`");
        }

        public Token Consume(TokenSet kinds)
        {
            var token = CurrentToken;
            if (kinds.Contains(token.Kind))
            {
                Offset += 1;
                return token;
            }

            throw new ParserException(token, $"expected {kinds}, but got `{token}`");
        }

        public bool Match(ToKind kind)
        {
            if (Current == kind)
            {
                Offset += 1;
                return true;
            }
            return false;
        }

        public bool Match(string text)
        {
            if (CurrentToken.Text == text)
            {
                Offset += 1;
                return true;
            }
            return false;
        }
    }
}
