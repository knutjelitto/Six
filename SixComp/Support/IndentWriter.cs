using System;
using System.IO;

namespace SixComp.Support
{
    public class IndentWriter : IWriter
    {
        const string prefix = "    ";
        string currentPrefix = string.Empty;
        bool pending = false;
        string lineend = Environment.NewLine;

        public IndentWriter(TextWriter writer)
        {
            Writer = writer;
        }

        public TextWriter Writer { get; }
        public int Level { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IDisposable Indent(Action? before = null, Action? after = null)
        {
            before?.Invoke();
            currentPrefix += prefix;
            return new Disposable(() =>
            {
                currentPrefix = currentPrefix.Substring(0, currentPrefix.Length - prefix.Length);
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

        public IDisposable Space()
        {
            Prefix();
            var oldLineend = lineend;
            lineend = " ";
            return new Disposable(() =>
            {
                lineend = oldLineend;
                WriteLine();
            });
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
            Writer.Write(lineend);
            pending = true;
        }

        public void WriteLine()
        {
            Writer.Write(lineend);
            pending = true;
        }

        private void Prefix()
        {
            if (pending)
            {
                if (lineend != " ")
                {
                    Writer.Write(currentPrefix);
                }
                pending = false;
            }
        }
    }
}
