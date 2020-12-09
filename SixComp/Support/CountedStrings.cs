using System.Collections.Generic;
using System.Linq;

namespace SixComp.Support
{
    public class CountedStrings
    {
        private readonly Dictionary<string, int> strings = new Dictionary<string, int>();

        public void Add(string str)
        {
            if (!strings.TryGetValue(str, out var _))
            {
                strings.Add(str, 0);
            }

            strings[str] += 1;
        }

        public void Clear()
        {
            strings.Clear();
        }

        public void Report(IWriter writer, string label)
        {
            if (strings.Count > 0)
            {
                writer.WriteLine(label);
                using (writer.Indent())
                {
                    foreach (var kv in strings.OrderBy(kv => kv.Key))
                    {
                        writer.WriteLine($"[{kv.Value,4}] {kv.Key}");
                    }
                }
            }
        }
    }
}
