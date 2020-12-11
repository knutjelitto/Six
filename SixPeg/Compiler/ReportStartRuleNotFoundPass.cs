// Copyright © John Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SixPeg.Compiler
{
    using System.Collections.Generic;
    using System.Linq;
    using SixPeg.Expressions;
    using SixPeg.Properties;

    internal class ReportStartRuleNotFoundPass : CompilePass
    {
        public override IReadOnlyList<string> BlockedByErrors => new[] { "PEG0001" };

        public override IReadOnlyList<string> ErrorsProduced => new[] { "PEG0003" };

        public override void Run(Grammar grammar, CompileResult result)
        {
            var knownRules = new HashSet<string>(grammar.Rules.Select(r => r.Identifier.Name));

            foreach (var setting in grammar.Settings)
            {
                if (setting.Key.Name == SettingName.Start)
                {
                    var name = setting.Value.ToString().Trim();
                    if (!knownRules.Contains(name))
                    {
                        result.AddCompilerError(setting.Key.Start, () => Resources.PEG0003_ERROR_RuleDoesNotExist, name);
                    }
                }
            }
        }
    }
}
