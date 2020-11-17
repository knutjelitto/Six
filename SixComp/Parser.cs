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

            infix.Add(ToKind.Equal, ((left, _) => AssignExpression.Parse(this, left), Precedence.Assignement));
            infix.Add(ToKind.LParent, ((left, _) => CallExpression.Parse(this, left), Precedence.Call));
            infix.Add(ToKind.LBracket, ((left, _) => IndexingExpression.Parse(this, left), Precedence.Index));
            infix.Add(ToKind.Dot, ((left, _) => SelectExpression.Parse(this, left), Precedence.Select));

            infix.Add(ToKind.Quest, (ParsePostfixOp, Precedence.Postfix));
            infix.Add(ToKind.Bang, (ParsePostfixOp, Precedence.Postfix));

            infix.Add(ToKind.Less, (ParseInfixOp, Precedence.Relation));
            infix.Add(ToKind.LessEqual, (ParseInfixOp, Precedence.Relation));
            infix.Add(ToKind.Greater, (ParseInfixOp, Precedence.Relation));
            infix.Add(ToKind.GreaterEqual, (ParseInfixOp, Precedence.Relation));

            infix.Add(ToKind.Minus, (ParseInfixOp, Precedence.Addition));
            infix.Add(ToKind.Plus, (ParseInfixOp, Precedence.Addition));

            infix.Add(ToKind.Asterisk, (ParseInfixOp, Precedence.Multiplication));
            infix.Add(ToKind.Slash, (ParseInfixOp, Precedence.Multiplication));
            infix.Add(ToKind.Percent, (ParseInfixOp, Precedence.Multiplication));
        }

        public Lexer Lexer { get; }

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

        public T? Try<T>(ToKind start, Func<Parser, T> parse)
            where T : class
        {
            if (Current == start)
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

        public ToKind Current => Ahead(0).Kind;
        public Token CurrentToken => Ahead(0);
        public ToKind Next => Ahead(1).Kind;

        public Token Ahead(int offset)
        {
            while (this.tokens.Count <= this.Offset + offset)
            {
                this.tokens.Add(Lexer.GetNext());
            }

            return this.tokens[this.Offset + offset];
        }

        public Token ConsumeAny()
        {
            var token = CurrentToken;
            Offset += 1;
            return token;
        }

        public Token Consume(ToKind kind)
        {
            var token = Ahead(0);
            if (token.Kind == kind)
            {
                Offset += 1;
                return token;
            }

            Console.WriteLine($"{token.Span.GetLine()}");
            throw new InvalidOperationException($"expected `{kind.GetRep()}`, but got `{token.Kind.GetRep()}`");
        }

        public Token Consume(TokenSet kinds)
        {
            var token = CurrentToken;
            if (kinds.Contains(token.Kind))
            {
                Offset += 1;
                return token;
            }

            Console.WriteLine($"{token.Span.GetLine()}");
            throw new InvalidOperationException($"expected {kinds}, but got `{token.Kind.GetRep()}`");
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

            return AnyPrimary.Parse(this);
        }

        public AnyExpression ParseExpression(int precedence)
        {
            var left = ParsePrefix();

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

        private AnyExpression ParsePostfixOp(AnyExpression left, int precedence)
        {
            var token = ConsumeAny();
            return new PostfixExpression(left, token);
        }
    }
}
