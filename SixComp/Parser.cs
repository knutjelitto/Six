using SixComp.ParseTree;
using SixComp.Support;
using System;
using System.Collections.Generic;

namespace SixComp
{
    public class Parser
    {
        public int Offset;

        private readonly Dictionary<ToKind, (Func<AnyExpression> parser, int precedence)> prefix =
            new Dictionary<ToKind, (Func<AnyExpression> parser, int precedence)>();

        private readonly Dictionary<ToKind, (Func<AnyExpression, int, AnyExpression> parser, int precedence)> infix =
            new Dictionary<ToKind, (Func<AnyExpression, int, AnyExpression> parser, int precedence)>();

        private readonly List<Token> tokens = new List<Token>();

        public Parser(Lexer lexer)
        {
            Lexer = lexer;
            Offset = 0;

            prefix.Add(ToKind.Minus, (ParsePrefixOp, Precedence.Prefix));
            prefix.Add(ToKind.Plus, (ParsePrefixOp, Precedence.Prefix));
            prefix.Add(ToKind.Bang, (ParsePrefixOp, Precedence.Prefix));
            prefix.Add(ToKind.Tilde, (ParsePrefixOp, Precedence.Prefix));

            infix.Add(ToKind.Equal, ((left, _) => AssignExpression.Parse(this, left), Precedence.Assignment));

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
        }

        public Lexer Lexer { get; }
        public Source Source => Lexer.Source;

        public Unit Parse()
        {
            return Unit.Parse(this);
        }

        public bool IsOperator(ToKind kind)
        {
            return prefix.ContainsKey(kind) || infix.ContainsKey(kind);
        }

        public bool IsOperator()
        {
            return IsOperator(Current);
        }

        public bool IsKeyword(ToKind kind)
        {
            return Lexer.IsKeyword(kind);
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

        public bool Adjacent => Ahead(-1).Span.End == Ahead(0).Span.Start;

        public Token Ahead(int offset)
        {
            while (this.tokens.Count <= this.Offset + offset)
            {
                this.tokens.Add(Lexer.GetNext());
            }

            return this.tokens[Offset + offset];
        }

        public Token At(int offset)
        {
            return Ahead(offset - Offset);
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

        private AnyExpression ParsePrefix()
        {
            if (prefix.TryGetValue(Current, out var prefixFun))
            {
                return prefixFun.parser();
            }

            if (Current == ToKind.Amper)
            {
                return InOutExpression.Parse(this);
            }

            return AnyPrimary.Parse(this);
        }

        public AnyExpression ParseExpression(int precedence)
        {
            var left = ParsePrefix();

            left = AnyPostfix.Parse(this, left);

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

            return left;
        }

        public AnyExpression ParseExpression()
        {
            return ParseExpression(0);
        }


        private AnyExpression ParsePrefixOp()
        {
            var token = ConsumeAny();
            var operand = ParseExpression(Precedence.Prefix);
            return new PrefixExpression(token, operand);
        }

        private AnyExpression ParseInfixOp(AnyExpression left, int precedence)
        {
            var token = ConsumeAny();
            var right = AnyExpression.Parse(this, precedence);
            return new InfixExpression(left, token, right);
        }

        private AnyExpression ParseRangeInfix(AnyExpression left, int precedence)
        {
            var exclusive = Current == ToKind.DotDotLess;
            Consume(new TokenSet(ToKind.DotDotDot, ToKind.DotDotLess));

            var offset = Offset;

            try
            {
                var right = AnyExpression.Parse(this, precedence);
                return new RangeExpression(left, right, exclusive);
            }
            catch
            {
                Offset = offset;
                return new RangeExpression(left, null, exclusive);
            }
        }
    }
}
