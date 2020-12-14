using System;
using System.Collections.Generic;

namespace Six.Support
{
    public class SourceIndex
    {
        public SourceIndex(Source source)
        {
            Source = source;
            Index = BuildIndex(Source);
        }

        public Source Source { get; }
        public List<int> Index { get; }

        private static List<int> BuildIndex(Source source)
        {
            var index = new List<int>();

            var offset = 0;
            var start = offset;
            while (offset < source.Length)
            {
                if (source.Content[offset] == '\n')
                {
                    index.Add(start);
                    start = offset + 1;
                }
                offset += 1;
            }
            index.Add(start);

            return index;
        }

        public (int lineNumber, int columnNumber, string line) GetInfo(int offset)
        {
            if (offset < 0 || offset > Source.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }

            var lineNo = GetLineIndex(offset);
            var start = Index[lineNo];
            var end = lineNo + 1 < Index.Count ? Index[lineNo + 1] : Source.Length;

            return (lineNo + 1, offset - start + 1, Source.Content.Substring(start, end - start).TrimEnd());
        }

        public string? GetLine(int lineIndex)
        {
            if (lineIndex >= 0 && lineIndex < Index.Count)
            {
                return GetInfo(Index[lineIndex]).line;
            }
            return null;
        }

        public int GetLineIndex(int offset)
        {
            if (offset < 0 || offset > Source.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }

            var lineNo = Index.BinarySearch(offset);
            if (lineNo < 0)
            {
                lineNo = ~lineNo - 1;
            }
            if (lineNo >= Index.Count)
            {
                return Index.Count;
            }
            return lineNo;
        }
    }
}
