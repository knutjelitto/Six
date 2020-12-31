using Six.Support;
using SixPeg.Expression;
using SixPeg.Visiting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SixPeg.Matchers
{
    public class Parser
    {
        public readonly List<MatchRule> Rules;
        private readonly Dictionary<Symbol, MatchRule> Index;

        public Parser()
        {
            Rules = new List<MatchRule>();
            Index = new Dictionary<Symbol, MatchRule>();
        }

        public MatchRule Start { get; set; }
        public MatchRule Space { get; set; }
        public MatchRule More { get; set; }

        public Parser Build(Grammar grammar)
        {
            foreach (var rule in grammar.Rules)
            {
                var matcher = new MatchRule(rule.Name)
                {
                    IsTerminal = rule is TerminalExpression,
                    IsSingle = rule.Attributes.Symbols.Any(s => s.Text == "single"),
                };

                if (matcher.IsSingle)
                {
                    Debug.Assert(true);
                }

                Add(matcher);

                if (rule.Name.Text == "_")
                {
                    Space = matcher;
                }

                if (Start == null)
                {
                    Start = matcher;
                }
            }

            Space = Special(grammar, "space") ?? Space;
            Start = Special(grammar, "start") ?? Start;
            More = Special(grammar, "more");

            Debug.Assert(Rules.Count == grammar.Rules.Count);

            var resolver = new ExpressionResolver(this);

            for (var index = 0; index < grammar.Rules.Count; index += 1)
            {
                Rules[index].Matcher = resolver.Resolve(grammar.Rules[index]);
            }

            return this;
        }

        private MatchRule Special(Grammar grammar, string name)
        {
            var specialOption = grammar.Options.Where(o => o.Name.Text == name).FirstOrDefault();
            if (specialOption != null && GetRule(specialOption.Value, out var special))
            {
                return special;
            }
            return null;
        }

        public bool GetRule(Symbol name, out MatchRule rule)
        {
            return Index.TryGetValue(name, out rule);
        }

        public void Add(MatchRule rule)
        {
            if (Index.TryGetValue(rule.Name, out var already))
            {
                new Error(rule.Name.Source).Report($"rule `{rule.Name}`", rule.Name.Start, rule.Name.Length);
                new Error(already.Name.Source).Report($"is alread defined here", already.Name.Start, already.Name.Length);
                throw new BailOutException();
            }

            Rules.Add(rule);
            Index.Add(rule.Name, rule);
        }

        public void Clear()
        {
            foreach (var rule in Rules)
            {
                rule.Clear();
            }
            AnyMatcher.Clear();
        }
    }
}
