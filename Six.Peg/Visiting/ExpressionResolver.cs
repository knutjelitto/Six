using Six.Peg.Runtime;
using Six.Support;
using SixPeg.Expression;
using SixPeg.Matchers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SixPeg.Visiting
{
    public sealed class ExpressionResolver : IExpressionVisitor<AnyMatcher>
    {
        private readonly Parser parser;
        private readonly Optimizer optimizer;

        public ExpressionResolver(Parser parser)
        {
            this.parser = parser;
            optimizer = new Optimizer(parser);
        }

        public AnyMatcher Resolve(AnyExpression expr)
        {
            return Optim(expr.Accept(this));
        }

        private AnyMatcher Optim(AnyMatcher matcher)
        {
            return optimizer.Optimize(matcher);
        }

        public AnyMatcher Visit(AndExpression expr)
        {
            return Optim(new MatchAnd(Optim(expr.Expression.Accept(this))));
        }

        public AnyMatcher Visit(CharacterClassExpression expr)
        {
            IReadOnlyList<AnyMatcher> matchers = expr.Ranges.Select(e => Optim(e.Accept(this))).ToArray();

            return expr.Ranges.Count switch
            {
                1 => Optim(matchers[0]),
                _ => Optim(new MatchChoice(matchers)),
            };
        }

        public AnyMatcher Visit(CharacterRangeExpression expr)
        {
            return expr.Min == expr.Max
                ? Optim(new MatchCharacterExact(expr.Min))
                : Optim(new MatchCharacterRange(expr.Min, expr.Max));
        }

        public AnyMatcher Visit(CharacterSequenceExpression expr)
        {
            return expr.Text.Length switch
            {
                1 => Optim(new MatchCharacterExact(expr.Text[0])),
                _ => Optim(new MatchCharacterSequence(expr.Text))
            };
        }

        public AnyMatcher Visit(ChoiceExpression expr)
        {
            IReadOnlyList<AnyMatcher> matchers = expr.Expressions.Select(e => Optim(e.Accept(this))).ToArray();

            return matchers.Count switch
            {
                1 => Optim(matchers[0]),
                _ => Optim(new MatchChoice(matchers))
            };
        }

        public AnyMatcher Visit(ErrorExpression expr)
        {
            return Optim(new MatchError(expr.Arguments));
        }

        public AnyMatcher Visit(NotExpression expr)
        {
            return Optim(new MatchNot(Optim(expr.Expression.Accept(this))));
        }

        public AnyMatcher Visit(QuantifiedExpression expr)
        {
            return (expr.Quantifier.Min, expr.Quantifier.Max) switch
            {
                (1, 1) => Optim(expr.Expression.Accept(this)),
                (0, 1) => Optim(new MatchZeroOrOne(Optim(expr.Expression.Accept(this)))),
                (0, null) => Optim(new MatchZeroOrMore(Optim(expr.Expression.Accept(this)))),
                (1, null) => Optim(new MatchOneOrMore(Optim(expr.Expression.Accept(this)))),
                _ => throw new NotImplementedException(),
            };
        }

        public AnyMatcher Visit(ReferenceExpression expr)
        {
            if (parser.GetRule(expr.Name, out var rule))
            {
                return new MatchReference(rule) { AlwaysSucceeds = rule == parser.Space };
            }

            new Error(expr.Name.Source).Report($"undefined rule `{expr.Name}`", expr.Name.Start, expr.Name.Length);
            throw new BailOutException();
        }

        public AnyMatcher Visit(RuleExpression expr)
        {
            return Optim(expr.Expression.Accept(this));
        }

        public AnyMatcher Visit(TerminalExpression expr)
        {
            return Optim(expr.Expression.Accept(this));
        }

        public AnyMatcher Visit(SequenceExpression expr)
        {
            var matchers = expr.Select(e => Optim(e.Accept(this))).ToArray();
            return expr.Count switch
            {
                0 => new MatchEpsilon(),
                1 => Optim(matchers[0]),
                _ => Optim(new MatchSequence(expr.Select(e => e.Accept(this)))),
            };
        }

        public AnyMatcher Visit(SpacedExpression expr)
        {
            if (!parser.GetRule(expr.Name, out var rule))
            {
                Debug.Assert(parser.Space != null);
                var matchSpace = new MatchReference(parser.Space) { AlwaysSucceeds = true };

                var text = expr.Name.Text[1..^1];
                Debug.Assert(text.Length >= 1);

                var matchText = text.Length == 1
                    ? Optim(new MatchCharacterExact(text[0]))
                    : Optim(new MatchCharacterSequence(text));

                var sequence = new List<AnyMatcher> { matchSpace, matchText };
                if (text.IsIdentifier())
                {
                    _ = parser.Keywords.Add(text);
                    if (parser.More != null)
                    {
                        sequence.Add(new MatchNot(new MatchReference(parser.More)));
                    }
                }

                rule = new MatchRule(expr.Name, true)
                {
                    Matcher = Optim(new MatchSequence(sequence)),
                };
                parser.Add(rule);
            }

            return new MatchReference(rule);
        }

        public AnyMatcher Visit(WildcardExpression expr)
        {
            return Optim(new MatchCharacterAny());
        }

        public AnyMatcher Visit(BeforeExpression expr)
        {
            return Optim(new MatchBefore(expr.Expression.Accept(this)));
        }
    }
}
