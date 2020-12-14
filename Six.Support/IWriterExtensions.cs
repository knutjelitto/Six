using System;

namespace Six.Support
{
    public static class IWriterExtensions
    {
        public static IDisposable Block(this IWriter writer)
        {
            return writer.Indent(() => writer.WriteLine("{"), () => writer.WriteLine("}"));
        }

        public static IDisposable Indent(this IWriter writer, string label)
        {
            writer.WriteLine(label);
            return writer.Indent();
        }
    }
}
