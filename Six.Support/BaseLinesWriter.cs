using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Six.Support
{
    public class BaseLinesWriter : IBaseWriter, IEnumerable<string>
    {
        private readonly List<string> lines = new List<string>();
        private StringBuilder current = new StringBuilder();

        public void Write(string text)
        {
            current.Append(text);
        }

        public void WriteLine()
        {
            lines.Add(current.ToString());
            current.Clear();
        }

        public IEnumerator<string> GetEnumerator() => lines.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
