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
    }
}
