using Six.Support;

namespace SixComp
{
    public class Error
    {
        public Error(Context context)
        {
            Context = context;
        }

        public Context Context { get; }
        public Source Source => Context.Source;
        public SourceIndex Index => Context.Index;

        public void Report(IWriter writer, string error, Token atToken) => Write(writer, error, atToken.Span.Start, atToken.Span.Length);

        public void Report(IWriter writer, string error, int start, int length = 1) => Write(writer, error, start, length);

        private void Write(IWriter writer, string error, int start, int length)
        {
            writer.WriteLine($"ERROR: {error}");
            var info = Index.GetInfo(start);
            writer.WriteLine($"    --> {Source.Name}[{info.lineNumber},{info.columnNumber}]");
            if (info.lineNumber == 1)
            {
                writer.WriteLine($"     |");
            }
            else
            {
                for (var i = info.lineNumber - 1 - 15; i < info.lineNumber - 1; i += 1)
                {
                    var line = Index.GetLine(i);
                    if (line != null)
                    {
                        writer.WriteLine($"     | {line}");
                    }
                }
            }
            writer.WriteLine($"{info.lineNumber,4} | {info.line}");
            var arrow = length > 1 ? $"^{new string('-', length - 2)}^" : "^";
            writer.WriteLine($"     = {new string(' ', info.columnNumber - 1)}{arrow}");
            writer.WriteLine($"     = {new string(' ', info.columnNumber - 1)}`-- {error}");
            //writer.WriteLine($"     =");
            for (var i = info.lineNumber; i < info.lineNumber + 3; i += 1)
            {
                var line = Index.GetLine(i);
                if (line != null)
                {
                    writer.WriteLine($"     | {line}");
                }
            }
        }
    }
}
