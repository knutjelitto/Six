// Copyright © John Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SixPeg.Compiler
{
    using System.Collections.Generic;
    using System.Linq;
    using SixPeg.Expressions;
    using SixPeg.Properties;

    internal class ReportUnusedRulesPass : CompilePass
    {
        public override IReadOnlyList<string> BlockedByErrors => new[] { "PEG0001", "PEG0002", "PEG0003", "PEG0005" };

        public override IReadOnlyList<string> ErrorsProduced => new[] { "PEG0017" };

        public override void Run(Grammar grammar, CompileResult result) => new UnusedRulesExpressionTreeWalker(result).WalkGrammar(grammar);

        private class UnusedRulesExpressionTreeWalker : ExpressionTreeWalker
        {
            private CompileResult result;
            private Queue<string> rulesToVisit = new Queue<string>();
            private HashSet<string> usedRules = new HashSet<string>();

            public UnusedRulesExpressionTreeWalker(CompileResult result)
            {
                this.result = result;
            }

            public override void WalkGrammar(Grammar grammar)
            {
                var rules = grammar.Rules.ToDictionary(r => r.Identifier.Name);
                var visibleRules = PublicRuleFinder.Find(grammar);

                if (visibleRules.StartRule != null)
                {
                    this.usedRules.Add(visibleRules.StartRule.Identifier.Name);
                    this.rulesToVisit.Enqueue(visibleRules.StartRule.Identifier.Name);
                }

                foreach (var rule in visibleRules.PublicRules.Concat(visibleRules.ExportedRules))
                {
                    if (this.usedRules.Add(rule.Identifier.Name))
                    {
                        this.rulesToVisit.Enqueue(rule.Identifier.Name);
                    }
                }

                while (this.rulesToVisit.Count > 0)
                {
                    var ruleName = this.rulesToVisit.Dequeue();
                    this.WalkRule(rules[ruleName]);
                }

                var unusedRules = new HashSet<string>(rules.Keys);
                unusedRules.ExceptWith(this.usedRules);

                foreach (var ruleName in unusedRules)
                {
                    var rule = rules[ruleName];
                    this.result.AddCompilerError(rule.Identifier.Start, () => Resources.PEG0017_WARNING_UnusedRule, rule.Identifier.Name);
                }
            }

            protected override void WalkNameExpression(NameExpression nameExpression)
            {
                var name = nameExpression.Identifier.Name;
                if (this.usedRules.Add(name))
                {
                    this.rulesToVisit.Enqueue(name);
                }
            }
        }
    }
}
