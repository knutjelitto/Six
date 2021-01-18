namespace Six.Peg.Runtime
{
    public class Context : ISource
    {
        public readonly string Text;
        public readonly int Length;

        public Context(string fileName, Options options = null)
            : this(new Source(fileName), options ?? new Options())
        {
        }

        protected Context(Source source, Options options)
        {
            Source = source;
            Options = options;
            Text = Source.Text;
            Length = Text.Length;
        }

        public Source Source { get; }
        public Options Options { get; }

        string ISource.Name => Source.Name;
        string ISource.Text => Source.Text;
        SourceIndex ISource.Index => Source.Index;
    }
}
