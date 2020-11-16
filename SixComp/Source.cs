namespace SixComp
{
    public class Source
    {
        private readonly string name;
        private readonly string content;

        public Source(string name, string content)
        {
            this.name = name;
            this.content = content;
        }

        public int Lenght => content.Length;
        public char this[int index] => content[index];

        public string Chars(Span span)
        {
            return content.Substring(span.Start, span.Length);
        }

        public string LineFor(Span span)
        {
            var start = content.LastIndexOfAny(new char[] { '\n', '\r' }, span.Start);
            start = start < 0 ? 0 : start;
            var end = content.IndexOfAny(new char[] { '\n', '\r' }, span.End);
            end = end < 0 ? content.Length : end;

            return content.Substring(start, end - start);
        }
    }
}
