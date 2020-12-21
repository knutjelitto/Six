using Six.Support;
using SixPeg.Matches;

namespace SixPeg.Expression
{
    public class ResolveVisitor : IVisitor<bool>
    {
        private readonly GrammarExpression grammar;
        private readonly IWriter writer;

        public ResolveVisitor(GrammarExpression grammar, IWriter writer)
        {
            this.grammar = grammar;
            this.writer = writer;
        }

        public void Resolve()
        {
            _ = Visit(grammar);
        }

        public bool Visit(GrammarExpression expr)
        {
            expr.Grammar = grammar;

            grammar.Space = null;
            grammar.Start = null;
            grammar.Indexed.Clear();

            foreach (var rule in grammar.Rules)
            {
                if (grammar.Indexed.TryGetValue(rule.Name, out var already))
                {
                    writer.WriteLine($"already defined rule: {already.Name}");
                    grammar.Error = true;
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
            }

            var index = 0;
            while (index < grammar.Rules.Count)
            {
                var rule = grammar.Rules[index];
                _ = rule.Accept(this);
                grammar.Caches.Add(rule.Name, new MatchCache(rule.Name));
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
