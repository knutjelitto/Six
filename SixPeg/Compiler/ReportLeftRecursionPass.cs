// Copyright © John Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace Pegasus.Compiler
{
    using System.Collections.Generic;
    using System.Linq;
    using Pegasus.Expressions;
    using Pegasus.Properties;

    internal class ReportLeftRecursionPass : CompilePass
    {
        public override IReadOnlyList<string> BlockedByErrors => new[] { "PEG0001", "PEG0002", "PEG0003" };

        public override IReadOnlyList<string> ErrorsProduced => new[] { "PEG0020", "PEG0023" };

        public override void Run(Grammar grammar, CompileResult result)
        {
            foreach (var rule in result.LeftRecursiveRules)
            {
                if (!rule.Flags.Any(f => f.Name == "memoize"))
                {
                    result.AddCompilerError(rule.Identifier.Start, () => Resources.PEG0020_ERROR_UnmemoizedLeftRecursion, rule.Identifier.Name);
                }
            }

            foreach (var set in result.MutuallyRecursiveRules)
            {
                var ruleNames = string.Join(", ", set.Select(r => r.Identifier.Name));
                foreach (var rule in set)
                {
                    result.AddCompilerError(rule.Identifier.Start, () => Resources.PEG0023_ERROR_AmbiguousLeftRecursionDetected, rule.Identifier.Name, ruleNames);
                }
            }
        }
    }
}
