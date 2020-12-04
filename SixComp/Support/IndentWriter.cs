using System;

namespace SixComp.Support
{
    public class IndentWriter : IWriter
    {
        const string indent = "    ";
        string currentIndent = string.Empty;
        bool pending = false;

        public IndentWriter(IBaseWriter writer)
        {
            Writer = writer;
        }

        public IBaseWriter Writer { get; }

        public IDisposable Indent(Action? before = null, Action? after = null)
        {
            before?.Invoke();
            currentIndent += indent;
            return new Disposable(() =>
            {
                currentIndent = currentIndent.Substring(0, currentIndent.Length - indent.Length);
                after?.Invoke();
            });
        }

        public void Indent(Action action)
        {
            using (Indent())
            {
                action();
            }
        }

        public void Write(string text)
        {
            Prefix();
            Writer.Write(text);
        }

        public void WriteLine(string text)
        {
            Prefix();
            Writer.Write(text);
            Writer.WriteLine();
            pending = true;
        }

        public void WriteLine()
        {
            Writer.WriteLine();
            pending = true;
        }

        private void Prefix()
        {
            if (pending)
            {
                Writer.Write(currentIndent);
                pending = false;
            }
        }
    }
}
