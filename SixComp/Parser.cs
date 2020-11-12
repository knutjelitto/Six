using SixComp.ParseTree;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SixComp
{
    public class Parser
    {
        private int current;

        private readonly Dictionary<ToKind, (Func<Token, Expression> parser, int precedence)> prefix =
            new Dictionary<ToKind, (Func<Token, Expression> parser, int precedence)>();

        private readonly Dictionary<ToKind, (Func<Expression, Token, int, Expression> parser, int precedence)> infix =
            new Dictionary<ToKind, (Func<Expression, Token, int, Expression> parser, int precedence)>();

        private readonly List<Token> tokens = new List<Token>();

        public Parser(Lexer lexer)
        {
            Lexer = lexer;
            current = 0;

            prefix.Add(ToKind.Name, (ParseName, Precedence.Prefix));
            prefix.Add(ToKind.Number, (ParseNumber, Precedence.Prefix));

            prefix.Add(ToKind.Minus, (ParsePrefixOp, Precedence.Prefix));
            prefix.Add(ToKind.Plus, (ParsePrefixOp, Precedence.Prefix));
            prefix.Add(ToKind.Bang, (ParsePrefixOp, Precedence.Prefix));

            //infix.Add(ToKind.Quest, (ParsePostfixOp, Precedence.Postfix));

            infix.Add(ToKind.LParen, (ParseCall, Precedence.Call));
            infix.Add(ToKind.Quest, (ParseConditional, Precedence.Conditional));

            infix.Add(ToKind.Minus, (ParseInfixOp, Precedence.Addition));
            infix.Add(ToKind.Plus, (ParseInfixOp, Precedence.Addition));

            infix.Add(ToKind.Asterisk, (ParseInfixOp, Precedence.Multiplication));
            infix.Add(ToKind.Slash, (ParseInfixOp, Precedence.Multiplication));
            infix.Add(ToKind.Percent, (ParseInfixOp, Precedence.Multiplication));
        }

        public Lexer Lexer { get; }

        public Unit Parse()
        {
            return ParseUnit();
        }

        private Unit ParseUnit()
        {
            return new Unit(ParseDeclarations());

        }

        private DeclarationList ParseDeclarations()
        {
            var declarations = new List<Declaration>();

            while (TryParseDeclaration() is Declaration declaration)
            {
                declarations.Add(declaration);
            }

            return new DeclarationList(declarations);
        }

        private Declaration? TryParseDeclaration()
        {
            var token = Ahead(0);
            switch (token.Kind)
            {
                case ToKind.KwLet:
                    return ParseLetDeclaration();
                case ToKind.KwVar:
                    break;
                case ToKind.KwFunc:
                    break;
                case ToKind.KwClass:
                    return ParseClassDeclaration();
                case ToKind.KwStruct:
                    return ParseStructDeclaration();
                case ToKind.KwEnum:
                    return ParseEnumDeclaration();
            }

            return null;
        }

        private LetDeclaration ParseLetDeclaration()
        {
            Consume(ToKind.KwLet);
            var name = ParseName();
            var type = TryParseTypeAnnotation();
            var init = TryParseInitializer();

            return new LetDeclaration(name, type, init);
        }

        private ClassDeclaration ParseClassDeclaration()
        {
            Consume(ToKind.KwClass);
            var name = ParseName();
            Consume(ToKind.LBrace);
            var declarations = ParseDeclarations();
            Consume(ToKind.RBrace);

            return new ClassDeclaration(name, declarations);
        }

        private StructDeclaration ParseStructDeclaration()
        {
            Consume(ToKind.KwStruct);
            var name = ParseName();
            Consume(ToKind.LBrace);
            var declarations = ParseDeclarations();
            Consume(ToKind.RBrace);

            return new StructDeclaration(name, declarations);
        }

        private EnumDeclaration ParseEnumDeclaration()
        {
            Consume(ToKind.KwEnum);
            var name = ParseName();
            var generics = Try(ToKind.Less, ParseGenericParameterClause);
            Consume(ToKind.LBrace);
            var cases = ParseEnumCases();
            Consume(ToKind.RBrace);

            return new EnumDeclaration(name, generics, cases);
        }

        private DeclarationList ParseEnumCases()
        {
            var cases = new List<Declaration>();

            while (Current.Kind == ToKind.KwCase)
            {
                do
                {
                    Consume();

                    var @case = ParseEnumCase();
                    cases.Add(@case);
                }
                while (Current.Kind == ToKind.Comma);
            }

            return new DeclarationList(cases);
        }

        private EnumCase ParseEnumCase()
        {
            var name = Consume(ToKind.Name);
            var types = Try(ToKind.LParen, ParseTupleType);

            return new EnumCase(name, types);
        }

        private IType? TryParseTypeAnnotation()
        {
            if (Current.Kind == ToKind.Colon)
            {
                return ParseTypeAnnotation();
            }
            return null;
        }

        private IType ParseTypeAnnotation()
        {
            Consume(ToKind.Colon);
            return ParseType();
        }

        private GenericParameterList ParseGenericParameterClause()
        {
            List<GenericParameter> names = new List<GenericParameter>();

            Consume(ToKind.Less);
            do
            {
                var parameter = ParseGenericParameter();
                names.Add(parameter);
            }
            while (Match(ToKind.Comma));
            Consume(ToKind.Greater);

            return new GenericParameterList(names);
        }

        private TypeList ParseGenericArgumentClause()
        {
            var types = new List<IType>();
            Consume(ToKind.Less);
            do
            {
                var type = ParseType();
                types.Add(type);
            }
            while (Match(ToKind.Comma));
            Consume(ToKind.Greater);

            return new TypeList(types);
        }

        private GenericParameter ParseGenericParameter()
        {
            var name = ParseName();
            return new GenericParameter(name);
        }

        private Name ParseName()
        {
            return new Name(Consume(ToKind.Name));
        }

        private IType ParseType()
        {
            var names = new List<TypeName>();

            do
            {
                var name = ParseTypeName();
                names.Add(name);
            }
            while (Match(ToKind.Dot));

            return new TypeIdentifier(names);
        }

        private TypeName ParseTypeName()
        {
            var name = ParseName();
            var arguments = Try(ToKind.Less, ParseGenericArgumentClause);

            return new TypeName(name, arguments);
        }

        private Expression? TryParseInitializer()
        {
            if (Current.Kind == ToKind.Assign)
            {
                Consume();
                return ParseExpression();
            }
            return null;
        }

        private T? Try<T>(ToKind start, Func<T> parse)
            where T : class
        {
            if (Current.Kind == start)
            {
                return parse();
            }
            return null;
        }

        private TupleType ParseTupleType()
        {
            var items = new List<TupleTypeItem>();
            Consume(ToKind.LParen);
            while (Current.Kind != ToKind.RParent)
            {
                var item = ParseTupleTypeItem();
                items.Add(item);
                if (Current.Kind != ToKind.RParent)
                {
                    Consume(ToKind.Comma);
                }
            }
            Consume(ToKind.RParent);

            return new TupleType(items);
        }


        private TupleTypeItem ParseTupleTypeItem()
        {
            Name? name = null;

            if (Ahead(1).Kind == ToKind.Colon)
            {
                name = ParseName();
                Consume(ToKind.Colon);
            }

            var type = ParseType();

            return new TupleTypeItem(name, type);

        }

        public Token Current => Ahead(0);

        public Token Ahead(int offset)
        {
            while (tokens.Count <= current + offset)
            {
                tokens.Add(Lexer.Next());
            }

            return tokens[current + offset];
        }

        private Token Consume()
        {
            var token = Ahead(0);
            current += 1;
            return token;
        }

        private Token Consume(ToKind kind)
        {
            var token = Ahead(0);
            if (token.Kind == kind)
            {
                current += 1;
                return token; ;
            }

            throw new InvalidOperationException($"expected {kind}, but got {token.Kind}");
        }

        private bool Match(ToKind kind)
        {
            var token = Ahead(0);
            if (token.Kind == kind)
            {
                current += 1;
                return true;
            }
            return false;
        }

        private Expression ParseExpression(int precedence)
        {
            var token = Consume();

            if (!prefix.TryGetValue(token.Kind, out var prefixFun))
            {
                throw new InvalidOperationException($"couldn't continue on {token.Kind}");
            }

            var left = prefixFun.parser(token);

            while (true)
            {
                if (!infix.TryGetValue(Ahead(0).Kind, out var infixFun) || precedence > infixFun.precedence)
                {
                    break;
                }
                else
                {
                    left = infixFun.parser(left, Consume(), infixFun.precedence);
                    //left = infix.Parse(this, left, Consume());
                }
            }

            return left;
        }

        private Expression ParseExpression()
        {
            return ParseExpression(0);
        }


        private Expression ParsePrefixOp(Token token)
        {
            var operand = ParseExpression(Precedence.Prefix);
            return new PrefixExpression(token, operand);
        }

        private Expression ParseName(Token token)
        {
            Debug.Assert(token.Kind == ToKind.Name);
            return new NameExpression(token);
        }

        private Expression ParseNumber(Token token)
        {
            Debug.Assert(token.Kind == ToKind.Number);
            return new NumberLiteralExpression(token);
        }

        private Expression ParseInfixOp(Expression left, Token token, int precedence)
        {
            var right = ParseExpression(precedence);
            return new InfixExpression(left, token, right);
        }

        private Expression ParsePostfixOp(Expression left, Token token, int precedence)
        {
            return new PostfixExpression(left, token);
        }

        private Expression ParseConditional(Expression left, Token token, int precedence)
        {
            Debug.Assert(token.Kind == ToKind.Quest);
            var ifTrue = ParseExpression();
            Consume(ToKind.Colon);
            var iffalse = ParseExpression(precedence - 1);

            return new ConditionalExpression(left, ifTrue, iffalse);
        }

        private Expression ParseCall(Expression left, Token token, int precedence)
        {
            Debug.Assert(token.Kind == ToKind.LParen);

            var args = new List<Expression>();

            if (!Match(ToKind.RParent))
            {
                do
                {
                    args.Add(ParseExpression());
                }
                while (Match(ToKind.Colon));
                Consume(ToKind.RParent);

            }

            return new CallExpression(left, args);
        }


        private abstract class Parselet
        {
        }

        private abstract class InfixParselet : Parselet
        {
            public abstract Expression Parse(Parser parser, Expression left, Token token);
            public abstract int Prec { get; }
        }

        private class InfixOpParselet : InfixParselet
        {
            public InfixOpParselet(int precedence, bool isRight)
            {
                Prec = precedence;
                IsRight = isRight;
            }

            public override int Prec { get; }
            public bool IsRight { get; }

            public override Expression Parse(Parser parser, Expression left, Token token)
            {
                var right = parser.ParseExpression(Prec - (IsRight ? 1 : 0));
                return new InfixExpression(left, token, right);
            }
        }

        private class CallParselet : InfixParselet
        {
            public override int Prec => Precedence.Call;

            public override Expression Parse(Parser parser, Expression left, Token token)
            {
                var args = new List<Expression>();

                if (!parser.Match(ToKind.RParent))
                {
                    do
                    {
                        args.Add(parser.ParseExpression());
                    }
                    while (parser.Match(ToKind.Colon));
                    parser.Consume(ToKind.RParent);

                }

                return new CallExpression(left, args);
            }
        }

        private class PostfixParselet : InfixParselet
        {
            public override int Prec => Precedence.Postfix;

            public override Expression Parse(Parser parser, Expression left, Token token)
            {
                return new PostfixExpression(left, token);
            }
        }

        private class ConditionalParselet : InfixParselet
        {
            public override int Prec => Precedence.Conditional;

            public override Expression Parse(Parser parser, Expression left, Token token)
            {
                var ifTrue = parser.ParseExpression();
                parser.Consume(ToKind.Colon);
                var iffalse = parser.ParseExpression(Prec - 1);

                return new ConditionalExpression(left, ifTrue, iffalse);
            }
        }
    }
}
