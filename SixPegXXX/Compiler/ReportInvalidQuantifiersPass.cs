// Copyright © John Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SixPeg.Compiler
{
    using System.Collections.Generic;
    using SixPeg.Expressions;
    using SixPeg.Properties;

    internal class ReportInvalidQuantifiersPass : CompilePass
    {
        public override IReadOnlyList<string> BlockedByErrors => new[] { "PEG0001" };

        public override IReadOnlyList<string> ErrorsProduced => new[] { "PEG0015", "PEG0024" };

        public override void Run(Grammar grammar, CompileResult result) => new InvalidQuantifierTreeWalker(result).WalkGrammar(grammar);

        private class InvalidQuantifierTreeWalker : ExpressionTreeWalker
        {
            private readonly CompileResult result;

            public InvalidQuantifierTreeWalker(CompileResult result)
            {
                this.result = result;
            }

            protected override void WalkRepetitionExpression(RepetitionExpression repetitionExpression)
            {
                if (repetitionExpression.Quantifier.Max == 0 ||
                    repetitionExpression.Quantifier.Max < repetitionExpression.Quantifier.Min)
                {
                    this.result.AddCompilerError(repetitionExpression.Quantifier.Start, () => Resources.PEG0015_WARNING_QuantifierInvalid);
                }

                if (repetitionExpression.Quantifier.Max == 1 &&
                    repetitionExpression.Quantifier.Delimiter != null)
                {
                    this.result.AddCompilerError(repetitionExpression.Quantifier.Start, () => Resources.PEG0024_WARNING_UnusedDelimiter);
                }

                base.WalkRepetitionExpression(repetitionExpression);
            }
        }
    }
}
