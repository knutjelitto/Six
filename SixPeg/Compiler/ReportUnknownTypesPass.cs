// Copyright © John Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SixPeg.Compiler
{
    using System.Collections.Generic;
    using SixPeg.Expressions;
    using SixPeg.Properties;

    internal class ReportUnknownTypesPass : CompilePass
    {
        public override IReadOnlyList<string> BlockedByErrors => new[] { "PEG0001", "PEG0002", "PEG0003" };

        public override IReadOnlyList<string> ErrorsProduced => new[] { "PEG0019" };

        public override void Run(Grammar grammar, CompileResult result)
        {
            var types = result.ExpressionTypes;

            foreach (var rule in grammar.Rules)
            {
                if (!types.ContainsKey(rule.Expression))
                {
                    result.AddCompilerError(rule.Identifier.Start, () => Resources.PEG0019_ERROR_UnknownType, rule.Identifier.Name);
                }
            }
        }
    }
}
