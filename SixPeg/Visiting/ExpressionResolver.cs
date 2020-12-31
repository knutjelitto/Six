﻿using Six.Support;
using SixPeg.Expression;
using SixPeg.Matchers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SixPeg.Visiting
{
    public class ExpressionResolver : IExpressionVisitor<AnyMatcher>
    {
        private readonly Parser parser;

        public ExpressionResolver(Parser parser)
        {
            this.parser = parser;
        }

        public AnyMatcher Resolve(AnyExpression expr)
        {
            return expr.Accept(this);
        }

        public AnyMatcher Visit(AndExpression expr)
        {
            return new MatchAnd(expr.Expression.Accept(this));
        }

        public AnyMatcher Visit(CharacterClassExpression expr)
        {
            return new MatchChoice(expr.Ranges.Select(r => r.Accept(this)));
        }

        public AnyMatcher Visit(CharacterRangeExpression expr)
        {
            return expr.Min == expr.Max
                ? new MatchCharacterExact(expr.Min)
                : (AnyMatcher)new MatchCharacterRange(expr.Min, expr.Max);
        }

        public AnyMatcher Visit(CharacterSequenceExpression expr)
        {
            return expr.Text.Length switch
            {
                1 => new MatchCharacterExact(expr.Text[0]),
                _ => new MatchCharacterSequence(expr.Text)
            };
        }

        public AnyMatcher Visit(ChoiceExpression expr)
        {
            return expr.Count switch
            {
                1 => expr[0].Accept(this),
                _ => new MatchChoice(expr.Expressions.Select(e => e.Accept(this)))
            };
        }

        public AnyMatcher Visit(ErrorExpression expr)
        {
            return new MatchError(expr.Arguments);
        }

        public AnyMatcher Visit(NotExpression expr)
        {
            return new MatchNot(expr.Expression.Accept(this));
        }

        public AnyMatcher Visit(QuantifiedExpression expr)
        {
            return (expr.Quantifier.Min, expr.Quantifier.Max) switch
            {
                (1, 1) => expr.Expression.Accept(this),
                (0, 1) => new MatchZeroOrOne(expr.Expression.Accept(this)),
                (0, null) => new MatchZeroOrMore(expr.Expression.Accept(this)),
                (1, null) => new MatchOneOrMore(expr.Expression.Accept(this)),
                _ => throw new NotImplementedException(),
            };
        }

        public AnyMatcher Visit(ReferenceExpression expr)
        {
            if (parser.GetRule(expr.Name, out var rule))
            {
                return new MatchReference(rule);
            }

            new Error(expr.Name.Source).Report($"undefined rule `{expr.Name}`", expr.Name.Start, expr.Name.Length);
            throw new BailOutException();
        }

        public AnyMatcher Visit(RuleExpression expr)
        {
            return expr.Expression.Accept(this);
        }

        public AnyMatcher Visit(TerminalExpression expr)
        {
            return expr.Expression.Accept(this);
        }

        public AnyMatcher Visit(SequenceExpression expr)
        {
            return expr.Count switch
            {
                0 => new MatchEpsilon(),
                1 => expr[0].Accept(this),
                _ => new MatchSequence(expr.Select(e => e.Accept(this)))
            };
        }

        public AnyMatcher Visit(SpacedExpression expr)
        {
            if (!parser.GetRule(expr.Name, out var rule))
            {
                var matchSpace = new MatchReference(parser.Space);
                Debug.Assert(matchSpace != null);

                var text = expr.Name.Text[1..^1];
                Debug.Assert(text.Length >= 1);

                var matchText = text.Length == 1
                    ? (AnyMatcher)new MatchCharacterExact(text[0])
                    : new MatchCharacterSequence(text);

                var sequence = new List<AnyMatcher> { matchSpace, matchText };
                if (parser.More != null)
                {
                    sequence.Add(new MatchNot(new MatchReference(parser.More)));
                }

                rule = new MatchRule(expr.Name)
                {
                    Matcher = new MatchSequence(sequence),
                    IsTerminal = true,
                };
                parser.Add(rule);
            }

            return new MatchReference(rule);
        }

        public AnyMatcher Visit(WildcardExpression expr)
        {
            return new MatchCharacterAny();
        }

        public AnyMatcher Visit(BeforeExpression expr)
        {
            return new MatchBefore(expr.Expression.Accept(this));
        }
    }
}
