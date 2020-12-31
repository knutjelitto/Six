using Six.Support;
using SixPeg.Expression;
using SixPeg.Matches;

namespace SixPeg.Visiting
{
    public class ResolveVisitor : IExpressionVisitor<bool>
    {
        private readonly Grammar grammar;
        private readonly IWriter writer;

        public ResolveVisitor(Grammar grammar, IWriter writer)
        {
            this.grammar = grammar;
            this.writer = writer;
        }

        public bool Resolve()
        {
            grammar.Space = null;
            grammar.Start = null;
            grammar.Indexed.Clear();

            foreach (var rule in grammar.Rules)
            {
                if (grammar.Indexed.TryGetValue(rule.Name, out var already))
                {
                    new Error(rule.Name.Source).Report($"rule `{rule.Name}`", rule.Name.Start);
                    new Error(already.Name.Source).Report($"is alread defined here", already.Name.Start);
                    writer.WriteLine();
                    grammar.Error = true;
                    throw new BailOutException();
                }
                else
                {
                    grammar.Indexed.Add(rule.Name, rule);
                }

                if (rule.Name.Text == "_")
                {
                    grammar.Space = rule;
                }

                if (grammar.Start == null)
                {
                    grammar.Start = rule;
                }

                if (grammar.Error)
                {
                    break;
                }
            }

            var index = 0;
            while (index < grammar.Rules.Count)
            {
                var rule = grammar.Rules[index];
                _ = rule.Accept(this);
                grammar.CachedMatch.Add(rule.Name, new MatchCache(rule.Name));
                grammar.CachedMatches.Add(rule.Name, new MatchesCache(rule.Name));
                index += 1;
            }

            return !grammar.Error;
        }

        public bool Visit(ReferenceExpression expr)
        {
            expr.Grammar = grammar;
            expr.Rule = grammar.FindRule(expr.Name);
            return true;
        }

        public bool Visit(SpacedExpression expr)
        {
            expr.Grammar = grammar;
            expr.Expression = grammar.AddSpaced(expr);
            // resolve newly created expression
            return expr.Expression.Accept(this);
        }

        public bool Visit(ChoiceExpression expr)
        {
            expr.Grammar = grammar;
            expr.AcceptForAll(this);
            return true;
        }

        public bool Visit(SequenceExpression expr)
        {
            expr.Grammar = grammar;
            expr.AcceptForAll(this);
            return true;
        }

        public bool Visit(AndExpression expr)
        {
            expr.Grammar = grammar;
            return expr.Expression.Accept(this);
        }

        public bool Visit(NotExpression expr)
        {
            expr.Grammar = grammar;
            return expr.Expression.Accept(this);
        }

        public bool Visit(QuantifiedExpression expr)
        {
            expr.Grammar = grammar;
            return expr.Expression.Accept(this);
        }

        public bool Visit(RuleExpression expr)
        {
            expr.Grammar = grammar;
            return expr.Expression.Accept(this);
        }

        public bool Visit(TerminalExpression expr)
        {
            expr.Grammar = grammar;
            return expr.Expression.Accept(this);
        }

        public bool Visit(BeforeExpression expr)
        {
            expr.Grammar = grammar;
            return expr.Expression.Accept(this);
        }

        public bool Visit(CharacterClassExpression expr)
        {
            expr.Grammar = grammar;
            return true;
        }

        public bool Visit(CharacterRangeExpression expr)
        {
            expr.Grammar = grammar;
            return true;
        }

        public bool Visit(CharacterSequenceExpression expr)
        {
            expr.Grammar = grammar;
            return true;
        }

        public bool Visit(ErrorExpression expr)
        {
            expr.Grammar = grammar;
            return true;
        }

        public bool Visit(WildcardExpression expr)
        {
            return true;
        }
    }
}
