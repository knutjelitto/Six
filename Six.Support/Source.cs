using System;

namespace Six.Support
{
    public class Source
    {
        private readonly Lazy<SourceIndex> index;

        public Source(string name, string content)
        {
            Name = name;
            Content = content;
            index = new Lazy<SourceIndex>(() => new SourceIndex(this));
        }

        public string Name { get; }
        public string Content { get; }
        public int Length => Content.Length;
        public SourceIndex Index => index.Value;


        public char this[int index] => index < Content.Length ? Content[index] : '\0';

        public string Chars(ISpan span)
        {
            return Content.Substring(span.Start, span.Length);
        }

        public string LineFor(ISpan span)
        {
            var start = Content.LastIndexOfAny(new char[] { '\n', '\r' }, span.Start);
            start = start < 0 ? 0 : start;
            var end = Content.IndexOfAny(new char[] { '\n', '\r' }, span.End);
            end = end < 0 ? Content.Length : end;

            return Content.Substring(start, end - start);
        }
    }
}
