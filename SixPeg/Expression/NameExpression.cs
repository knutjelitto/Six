﻿using SixPeg.Matchers;
using System.Diagnostics;

namespace SixPeg.Expression
{
    public class NameExpression : AnyExpression
    {
        public NameExpression(Identifier name)
        {
            Name = name;
        }

        public Identifier Name { get; }
        public RuleExpression Rule { get; private set; }

        public override IMatcher GetMatcher()
        {
            Debug.Assert(Rule != null);
            return new MatchName(Rule);
        }

        public override void Resolve(GrammarExpression grammar)
        {
            Rule = grammar.FindRule(Name);
        }
    }
}
