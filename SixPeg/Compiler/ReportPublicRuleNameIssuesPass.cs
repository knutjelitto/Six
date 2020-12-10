// Copyright © John Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace Pegasus.Compiler
{
    using System.Collections.Generic;
    using System.Linq;
    using Pegasus.Expressions;
    using Pegasus.Properties;

    internal class ReportPublicRuleNameIssuesPass : CompilePass
    {
        public override IReadOnlyList<string> BlockedByErrors => new string[0];

        public override IReadOnlyList<string> ErrorsProduced => new[] { "PEG0025" };

        public override void Run(Grammar grammar, CompileResult result)
        {
            foreach (var rule in grammar.Rules)
            {
                if (char.IsLower(rule.Identifier.Name[0]))
                {
                    if (rule.Flags.Any(f => f.Name == "public"))
                    {
                        result.AddCompilerError(rule.Identifier.Start, () => Resources.PEG0025_WARNING_LowercasePublicRule, rule.Identifier.Name);
                    }
                    else if (rule.Flags.Any(f => f.Name == "export"))
                    {
                        result.AddCompilerError(rule.Identifier.Start, () => Resources.PEG0025_WARNING_LowercaseExportedRule, rule.Identifier.Name);
                    }
                }
            }
        }
    }
}
