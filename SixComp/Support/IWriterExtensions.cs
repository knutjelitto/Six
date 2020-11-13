using System;

namespace SixComp.Support
{
    public static class IWriterExtensions
    {
        public static IDisposable Block(this IWriter writer)
        {
            return writer.Indent(() => writer.WriteLine("{"), () => writer.WriteLine("}"));
        }
    }
}
