using SixComp.ParseTree;
using System;
using System.Collections.Generic;

namespace SixComp
{
    public class Parser
    {
        private int current;

        private readonly Dictionary<ToKind, (Func<AnyExpression> parser, int precedence)> prefix =
            new Dictionary<ToKind, (Func<AnyExpression> parser, int precedence)>();

        private readonly Dictionary<ToKind, (Func<AnyExpression, int, AnyExpression> parser, int precedence)> infix =
            new Dictionary<ToKind, (Func<AnyExpression, int, AnyExpression> parser, int precedence)>();

        private readonly List<Token> tokens = new List<Token>();

        public Parser(Lexer lexer)
        {
            Lexer = lexer;
            current = 0;

            prefix.Add(ToKind.Minus, (ParsePrefixOp, Precedence.Prefix));
            prefix.Add(ToKind.Plus, (ParsePrefixOp, Precedence.Prefix));
            prefix.Add(ToKind.Bang, (ParsePrefixOp, Precedence.Prefix));
            prefix.Add(ToKind.Tilde, (ParsePrefixOp, Precedence.Prefix));

            infix.Add(ToKind.Equal, ((left, _) => AssignExpression.Parse(this, left), Precedence.Assignement));
            infix.Add(ToKind.LParen, ((left, _) => CallExpression.Parse(this, left), Precedence.Call));
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
            return IsOperator(Current.Kind);
        }

        public T? Try<T>(ToKind start, Func<Parser, T> parse)
            where T : class
        {
            if (Current.Kind == start)
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
            if (Current.Kind == start)
            {
                return parse(this);
            }
            return new T();
        }

        public Token Current => Ahead(0);
        public Token Next => Ahead(1);

        public Token Ahead(int offset)
        {
            while (tokens.Count <= current + offset)
            {
                tokens.Add(Lexer.GetNext());
            }

            return tokens[current + offset];
        }

        public Token Consume()
        {
            var token = Current;
            current += 1;
            return token;
        }

        public Token Consume(ToKind kind)
        {
            var token = Ahead(0);
            if (token.Kind == kind)
            {
                current += 1;
                return token;
            }

            Console.WriteLine($"{token.Span.GetLine()}");
            throw new InvalidOperationException($"expected {kind}, but got {token.Kind}");
        }

        public bool Match(ToKind kind)
        {
            if (Current.Kind == kind)
            {
                current += 1;
                return true;
            }
            return false;
        }

        private AnyExpression ParsePrefix()
        {
            if (prefix.TryGetValue(Current.Kind, out var prefixFun))
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
                if (!infix.TryGetValue(Current.Kind, out var infixFun) || precedence > infixFun.precedence)
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
            var token = Consume();
            var operand = ParseExpression(Precedence.Prefix);
            return new PrefixExpression(token, operand);
        }

        private AnyExpression ParseInfixOp(AnyExpression left, int precedence)
        {
            var token = Consume();
            var right = AnyExpression.Parse(this, precedence);
            return new InfixExpression(left, token, right);
        }

        private AnyExpression ParsePostfixOp(AnyExpression left, int precedence)
        {
            var token = Consume();
            return new PostfixExpression(left, token);
        }
    }
}
