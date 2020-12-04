using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SixComp.Support
{
    public class LinesWriter : IndentWriter, IEnumerable<string>
    {
        public LinesWriter(string prefix = "")
            : base(new BaseLines(prefix))
        {
        }

        public IReadOnlyList<string> Lines => ((BaseLines)Writer).Lines;

        public IEnumerator<string> GetEnumerator() => Lines.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private class BaseLines : IBaseWriter
        {
            private readonly List<string> lines = new List<string>();
            private readonly string prefix;
            private readonly StringBuilder current = new StringBuilder();

            public BaseLines(string prefix = "")
            {
                this.prefix = prefix;
                current.Append(prefix);
            }

            public IReadOnlyList<string> Lines => this.lines;

            public void Write(string text)
            {
                current.Append(text);
            }

            public void WriteLine()
            {
                lines.Add(current.ToString());
                current.Clear();
                current.Append(prefix);
            }
        }
    }
}
