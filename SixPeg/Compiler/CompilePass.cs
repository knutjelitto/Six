// Copyright © John Gietzen. All Rights Reserved. This source is subject to the MIT license. Please see license.md for more information.

namespace SixPeg.Compiler
{
    using System.Collections.Generic;
    using SixPeg.Expressions;

    internal abstract class CompilePass
    {
        public abstract IReadOnlyList<string> BlockedByErrors { get; }

        public abstract IReadOnlyList<string> ErrorsProduced { get; }

        public abstract void Run(Grammar grammar, CompileResult result);
    }
}
