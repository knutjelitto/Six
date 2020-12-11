using System;
using System.Diagnostics;

using Microsoft.CodeAnalysis;


namespace SixComp
{
    [Generator]
    public class Gen : ISourceGenerator
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
