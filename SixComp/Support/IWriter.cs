using System;

namespace SixComp.Support
{
    public interface IWriter
    {
        void Write(string text);
        void WriteLine(string text);
        void WriteLine();

        IDisposable Indent();
    }
}
