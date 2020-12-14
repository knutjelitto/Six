using System;

namespace Six.Support
{
    public interface IWriter
    {
        void Write(string text);
        void WriteLine(string text);
        void WriteLine();

        IDisposable Indent(Action? before = null, Action? after = null);
        void Indent(Action action);
        IDisposable IndentWith(string indent);
    }
}
