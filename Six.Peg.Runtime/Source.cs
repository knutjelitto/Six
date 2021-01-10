using System;
using System.IO;

namespace Six.Peg.Runtime
{
    public class Source : ISource
    {
        public readonly string Text;
        private readonly Lazy<SourceIndex> index;

        public Source(string name, string content)
        {
            Name = name;
            Text = content;
            index = new Lazy<SourceIndex>(() => new SourceIndex(this));
        }

        public Source(string fileName)
            : this(fileName, File.ReadAllText(fileName))
        {

        }

        public string Name { get; }
        public int Length => Text.Length;
        public SourceIndex Index => index.Value;


        public char this[int index] => index < Text.Length ? Text[index] : '\0';

        public string Chars(ISpan span)
        {
            return Text.Substring(span.Start, span.Length);
        }

        string ISource.Name => Name;
        string ISource.Text => Text;
        SourceIndex ISource.Index => Index;
    }
}
