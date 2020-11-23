using SixComp.ParseTree;
using SixComp.Support;
using System;
using System.Collections.Generic;

namespace SixComp
{
    public class Parser
    {
        private readonly Lexer lexer;
        private readonly Tokens tokens;

        public int Offset;

        private readonly Dictionary<ToKind, (Func<AnyExpression, int, AnyExpression> parser, int precedence)> infix =
            new Dictionary<ToKind, (Func<AnyExpression, int, AnyExpression> parser, int precedence)>();

        public Parser(Context context)
        {
            Context = context;
            lexer = Context.Lexer;
            tokens = Context.Tokens;
            Offset = 0;

            infix.Add(ToKind.PlusEqual, (ParseInfixOp, Precedence.Assignment));
            infix.Add(ToKind.MinusEqual, (ParseInfixOp, Precedence.Assignment));
            infix.Add(ToKind.AsteriskEqual, (ParseInfixOp, Precedence.Assignment));
            infix.Add(ToKind.SlashEqual, (ParseInfixOp, Precedence.Assignment));
            infix.Add(ToKind.PercentEqual, (ParseInfixOp, Precedence.Assignment));

            infix.Add(ToKind.Quest, ((left, precedence) => TernaryExpression.Parse(this, left, precedence), Precedence.Ternary));

            infix.Add(ToKind.AmperAmper, (ParseInfixOp, Precedence.Conjunction));
            infix.Add(ToKind.VBarVBar, (ParseInfixOp, Precedence.Conjunction));

            infix.Add(ToKind.EqualEqual, (ParseInfixOp, Precedence.Comparison));
            infix.Add(ToKind.BangEqual, (ParseInfixOp, Precedence.Comparison));

            infix.Add(ToKind.Less, (ParseInfixOp, Precedence.Comparison));
            infix.Add(ToKind.LessEqual, (ParseInfixOp, Precedence.Comparison));
            infix.Add(ToKind.Greater, (ParseInfixOp, Precedence.Comparison));
            infix.Add(ToKind.GreaterEqual, (ParseInfixOp, Precedence.Comparison));

            infix.Add(ToKind.QuestQuest, (ParseInfixOp, Precedence.NilCoalescing));

            infix.Add(ToKind.KwIs, (ParseInfixCast, Precedence.Casting));
            infix.Add(ToKind.KwAs, (ParseInfixCast, Precedence.Casting));
            infix.Add(ToKind.KwAsForce, (ParseInfixCast, Precedence.Casting));
            infix.Add(ToKind.KwAsChain, (ParseInfixCast, Precedence.Casting));

            infix.Add(ToKind.DotDotDot, (ParseRangeInfix, Precedence.RangeFormation));
            infix.Add(ToKind.DotDotLess, (ParseRangeInfix, Precedence.RangeFormation));

            infix.Add(ToKind.Minus, (ParseInfixOp, Precedence.Addition));
            infix.Add(ToKind.AmperMinus, (ParseInfixOp, Precedence.Addition));
            infix.Add(ToKind.Plus, (ParseInfixOp, Precedence.Addition));
            infix.Add(ToKind.AmperPlus, (ParseInfixOp, Precedence.Addition));
            infix.Add(ToKind.VBar, (ParseInfixOp, Precedence.Addition));

            infix.Add(ToKind.Asterisk, (ParseInfixOp, Precedence.Multiplication));
            infix.Add(ToKind.AmperAsterisk, (ParseInfixOp, Precedence.Multiplication));
            infix.Add(ToKind.Slash, (ParseInfixOp, Precedence.Multiplication));
            infix.Add(ToKind.AmperSlash, (ParseInfixOp, Precedence.Multiplication));
            infix.Add(ToKind.Percent, (ParseInfixOp, Precedence.Multiplication));
            infix.Add(ToKind.AmperPercent, (ParseInfixOp, Precedence.Multiplication));
            infix.Add(ToKind.Amper, (ParseInfixOp, Precedence.Multiplication));

            infix.Add(ToKind.LessLessLess, (ParseInfixOp, Precedence.BitwiseShift));
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

        public bool IsKeyword(ToKind kind)
        {
            return lexer.IsKeyword(kind);
        }

        public bool IsKeyword()
        {
            return IsKeyword(Current);
        }

        public T? Try<T>(ToKind start, Func<Parser, T> parse)
            where T : class
        {
            if (Current == start)
            {
                return parse(this);
            }
            return null;
        }

#if false
        public T? Try<T>(string opChars, Func<Parser, T> parse)
            where T : class
        {
            if (CurrentToken.Text == opChars)
            {
                return parse(this);
            }
            return null;
        }
#endif

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

        public Token CurrentToken => Ahead(0);
        public Token NextToken => Ahead(1);

        public bool Adjacent => Ahead(-1).Span.End == Ahead(0).Span.Start;

        public Token Ahead(int offset)
        {
            return At(Offset + offset);
        }

        public Token At(int offset)
        {
            return tokens[offset];
        }

        public Token ConsumeAny()
        {
            var token = CurrentToken;
            Offset += 1;
            return token;
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

#if false
        public Token Consume(string opChars)
        {
            var token = CurrentToken;
            if (token.Text == opChars)
            {
                Offset += 1;
                return token;
            }

            throw new ParserException(token, $"expected `{opChars}`, but got `{token}`");
        }
#endif

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

        public AnyExpression? TryParseExpression(int precedence)
        {
            var offset = Offset;

            var tryKind = TryExpression.TryKind.None;

            if (Match(ToKind.KwTry))
            {
                if (Match(ToKind.Bang))
                {
                    tryKind = TryExpression.TryKind.TryForce;
                }
                else if (Match(ToKind.Quest))
                {
                    tryKind = TryExpression.TryKind.TryChain;
                }
                else
                {
                    tryKind = TryExpression.TryKind.Try;
                }
            }

            var left = (AnyExpression?)AnyPrefix.TryParse(this);

            if (left == null)
            {
                Offset = offset;
                return null;
            }

            while (true)
            {
                if (!infix.TryGetValue(Current, out var infixFun) || precedence > infixFun.precedence)
                {
                    break;
                }
                else
                {
                    left = infixFun.parser(left, infixFun.precedence);
                }
            }

            if (tryKind != TryExpression.TryKind.None)
            {
                return TryExpression.From(tryKind, left);
            }

            return left;
        }

        private AnyExpression ParseInfixOp(AnyExpression left, int precedence)
        {
            var token = ConsumeAny();
            var right = AnyExpression.TryParse(this, precedence) ?? throw new InvalidOperationException();
            return new InfixExpression(left, token, right);
        }

        private AnyExpression ParseInfixCast(AnyExpression left, int precedence)
        {
            var token = ConsumeAny();
            var right = AnyType.Parse(this);
            return new InfixCastExpression(left, token, right);
        }

        private AnyExpression ParseRangeInfix(AnyExpression left, int precedence)
        {
            var exclusive = Current == ToKind.DotDotLess;
            Consume(new TokenSet(ToKind.DotDotDot, ToKind.DotDotLess));

            var offset = Offset;

            var right = AnyExpression.TryParse(this, precedence);

            return new RangeExpression(left, right, exclusive);
        }
    }
}
