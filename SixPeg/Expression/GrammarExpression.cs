using Six.Support;
using SixPeg.Matchers;
using SixPeg.Matches;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Expression
{
    public class GrammarExpression : AnyExpression
    {
        public readonly Dictionary<Symbol, RuleExpression> Indexed = new Dictionary<Symbol, RuleExpression>();
        public readonly Dictionary<Symbol, MatchCache> Caches = new Dictionary<Symbol, MatchCache>();

        public GrammarExpression(IList<RuleExpression> rules)
        {
            Rules = rules.ToList();
            Generated = new List<RuleExpression>();
        }

        public List<RuleExpression> Rules { get; }
        public List<RuleExpression> Generated { get; }
        public RuleExpression Start { get; set; }
        public RuleExpression Space { get; set; }
        public IMatcher Matcher { get; private set; }
        public bool Error { get; set; } = false;

        public void Clear()
        {
            foreach (var cache in Caches.Values)
            {
                cache.Clear();
            }
            AnyMatcher.Clear();
        }

        protected override IMatcher MakeMatcher()
        {
            return Start.GetMatcher();
        }

        public void ReportMatchers(IWriter writer)
        {
            if (Error)
            {
                return;
            }
            bool more = false;
            foreach (var rule in Rules)
            {
                if (more)
                {
                    writer.WriteLine();
                }
                using (writer.Indent($"{rule.Name} ="))
                {
                    rule.GetMatcher().Write(writer);
                }
                more = true;
            }
        }

        public RuleExpression FindRule(Symbol name)
        {
            if (Indexed.TryGetValue(name, out var rule))
            {
                rule.Used = true;
                return rule;
            }
            else
            {
                Console.WriteLine($"undefined rule: {name}");
                Error = true;
                return null;
            }
        }

        public AnyExpression AddSpaced(SpacedExpression spaced)
        {
            var identifier = spaced.Name;

            if (!Indexed.TryGetValue(identifier, out var _))
            {
                var expression = new CharacterSequenceExpression(spaced.Name.Text[1..^1]) { Spaced = true };
                var rule = new RuleExpression(identifier, Enumerable.Empty<Symbol>(), expression);
                Indexed.Add(identifier, rule);
                Rules.Add(rule);
            }

            return new ReferenceExpression(identifier);
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
