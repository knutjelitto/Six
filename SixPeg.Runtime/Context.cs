using Six.Support;

namespace SixPeg.Runtime
{
    public class Context : ISource
    {
        public readonly string Text;
        public readonly int Length;

        public Context(string fileName)
            : this(new Source(fileName))
        {
        }

        protected Context(Source source)
        {
            Source = source;

            Text = Source.Text;
            Length = Text.Length;
        }

        public Source Source { get; }

        string ISource.Name => Source.Name;
        string ISource.Text => Source.Text;
        SourceIndex ISource.Index => Source.Index;
    }
}
