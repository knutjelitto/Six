﻿using Six.Peg.Runtime;
using Six.Peg.Expression;
using SixPeg.Visiting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SixPeg.Matchers
{
    public class Parser
    {
        public List<MatchRule> Rules { get; }
        public Dictionary<Symbol, MatchRule> Index { get; }
        public HashSet<string> Keywords { get; }

        public Parser(string name)
        {
            Name = name;

            Rules = new List<MatchRule>();
            Index = new Dictionary<Symbol, MatchRule>();
            Keywords = new HashSet<string>();
        }

        public string Name { get; }

        public MatchRule Start { get; set; }
        public MatchRule Space { get; set; }
        public MatchRule More { get; set; }

        public Parser Build(Grammar grammar)
        {
            var matchesFlag = GetFlag(grammar, "matches").Text ?? string.Empty;
            var allSingle = matchesFlag == "first";

            foreach (var rule in grammar.Rules)
            {
                var matcher = new MatchRule(rule.Name, rule is TerminalExpression)
                {
                    IsSingle = allSingle || GetAttribute(rule, "single"),
                    IsFragment = GetAttribute(rule, "fragment"),
                    Flatten = GetAttribute(rule, "flatten"),
                    Lift = GetAttribute(rule, "lift"),
                };

                if (matcher.Flatten)
                {
                    Debug.Assert(true);
                }

                if (matcher.IsFragment)
                {
                    Debug.Assert(true);
                }

                if (matcher.IsSingle)
                {
                    Debug.Assert(true);
                }

                if (matcher.Lift)
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

            Space = GetSpecialRule(grammar, "space") ?? Space;
            Space.AlwaysSucceeds = true;
            Start = GetSpecialRule(grammar, "start") ?? Start;
            More = GetSpecialRule(grammar, "more");

            Debug.Assert(Rules.Count == grammar.Rules.Count);

            var resolver = new ExpressionResolver(this);

            for (var index = 0; index < grammar.Rules.Count; index += 1)
            {
                Rules[index].Matcher = resolver.Resolve(grammar.Rules[index]);
            }

            new Terminator(this).Terminate();

            return this;
        }

        private bool GetAttribute(Rule rule, string attribute)
        {
            return rule.Attributes.Symbols.Any(s => s.Text == attribute);
        }

        private MatchRule GetSpecialRule(Grammar grammar, string name)
        {
            var specialOption = grammar.Options.Where(o => o.Name.Text == name).FirstOrDefault();
            if (specialOption != null && GetRule(specialOption.Value, out var special))
            {
                return special;
            }
            return null;
        }

        private Symbol GetFlag(Grammar grammar, string name)
        {
            var flagOption = grammar.Options.Where(o => o.Name.Text == name).FirstOrDefault();
            if (flagOption != null)
            {
                return flagOption.Value;
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

            rule.Index = Rules.Count;
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
