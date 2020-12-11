using System;
using System.Diagnostics;

using Microsoft.CodeAnalysis;

namespace SixPeg
{
    [Generator]
    public class Generator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            Trace.WriteLine("YYYY");
        }

        public void Execute(GeneratorExecutionContext context)
        {
            throw new NotImplementedException();
        }

    }
}
