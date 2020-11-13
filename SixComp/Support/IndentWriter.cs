using System;
using System.IO;

namespace SixComp.Support
{
    public class IndentWriter : IWriter
    {
        const string prefix = "    ";
        string currentPrefix = string.Empty;
        bool pending = false;

        public IndentWriter(TextWriter writer)
        {
            Writer = writer;
        }

        public TextWriter Writer { get; }
        public int Level { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IDisposable Indent()
        {
            currentPrefix += prefix;
            return new Disposable(() => currentPrefix = currentPrefix.Substring(0, currentPrefix.Length - prefix.Length));
        }

        public void Write(string text)
        {
            Prefix();
            Writer.Write(text);
        }

        public void WriteLine(string text)
        {
            Prefix();
            Writer.WriteLine(text);
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
                Writer.Write(currentPrefix);
                pending = false;
            }
        }
    }
}
