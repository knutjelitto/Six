// Copyright © John Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SixPeg.Compiler
{
    using System.Collections.Generic;
    using SixPeg.Expressions;
    using SixPeg.Properties;

    internal class ReportDuplicateRulesPass : CompilePass
    {
        public override IReadOnlyList<string> BlockedByErrors => new[] { "PEG0001" };

        public override IReadOnlyList<string> ErrorsProduced => new[] { "PEG0002" };

        public override void Run(Grammar grammar, CompileResult result)
        {
            var knownRules = new HashSet<string>();

            foreach (var rule in grammar.Rules)
            {
                if (!knownRules.Add(rule.Identifier.Name))
                {
                    var cursor = rule.Identifier.Start;
                    result.AddCompilerError(cursor, () => Resources.PEG0002_ERROR_RuleAlreadyDefined, rule.Identifier.Name);
                }
            }
        }
    }
}
