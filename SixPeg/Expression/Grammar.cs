using Six.Support;
using SixPeg.Matchers;
using SixPeg.Matches;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SixPeg.Expression
{
    public class Grammar
    {
        public readonly Dictionary<Symbol, AnyRule> Indexed = new Dictionary<Symbol, AnyRule>();
        public readonly Dictionary<Symbol, MatchCache> CachedMatch = new Dictionary<Symbol, MatchCache>();
        public readonly Dictionary<Symbol, MatchesCache> CachedMatches = new Dictionary<Symbol, MatchesCache>();
        public readonly List<MatchName> ReferencesToResolve = new List<MatchName>();

        public Grammar(IList<AnyRule> rules)
        {
            Rules = rules.ToList();
            Generated = new List<AnyRule>();
        }

        public List<AnyRule> Rules { get; }
        public List<AnyRule> Generated { get; }
        public AnyRule Start { get; set; }
        public AnyRule Space { get; set; }
        public IMatcher Matcher { get; private set; }
        public bool Error { get; set; } = false;

        public void Clear()
        {
            foreach (var cache in CachedMatch.Values)
            {
                cache.Clear();
            }
            foreach (var cache in CachedMatches.Values)
            {
                cache.Clear();
            }
            AnyMatcher.Clear();
        }

        public IMatcher GetMatcher()
        {
            return Start.GetMatcher();
        }

        public void ResolveReferences()
        {
            if (Error)
            {
                return;
            }
            foreach (var rule in Rules)
            {
                _ = rule.GetMatcher();
            }

            foreach (var matcher in ReferencesToResolve)
            {
                matcher.SetMatcher(Indexed[matcher.Name].GetMatcher());
            }
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

        public AnyRule FindRule(Symbol name)
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
                var rule = new RuleExpression(identifier, expression);
                Indexed.Add(identifier, rule);
                Rules.Add(rule);
            }

            return new ReferenceExpression(identifier);
        }
    }
}
