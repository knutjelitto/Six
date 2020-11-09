using System;
using System.Collections.Generic;
using System.IO;

namespace Six
{
    public class GenVM
    {
        private class OP : Named
        {
            public OP(String name, int bytes, int number)
                : base(name)
            {
                Bytes = bytes;
                Number = number;

                Symbol = $"Op{Name}";
                Exec = $"Exec{Name}";
                Emit = $"Emit{Name}";
            }

            public Int32 Bytes { get; }
            public int Number { get; }
            public string Symbol { get; }
            public string Exec { get; }
            public string Emit { get; }
        }

        private class Named : Snippet
        {
            public String Name { get; }

            public Named(string name)
            {
                Name = name;
            }

            public override String ToString()
            {
                return $"{Name} ({Lines.Count})";
            }
        }

        private class Snippet
        {
            public List<string> Lines { get; }

            public Snippet()
            {
                Lines = new List<string>();
            }

            public void Add(string line)
            {
                Lines.Add(line);
            }
        }

        public void Ops()
        {
            const string marker = ";;== ";

            var opSnippets = "OP.snippets.asm";

            var snips = new List<OP>();
            OP snip = null;

            foreach (var line in File.ReadLines(opSnippets))
            {
                if (line.Trim(' ', '\t').Length == 0)
                {
                    continue;
                }

                if (line.StartsWith(marker))
                {
                    var x = line.Substring(marker.Length).Trim();
                    var parts = line.Substring(marker.Length).Trim().Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    snip = new OP(parts[0], int.Parse(parts[1]), snips.Count);
                    snips.Add(snip);
                }
                else
                {
                    snip.Add(line.TrimEnd());
                }
            }

            Temple(snips);
        }

        private void Temple(List<OP> ops)
        {
            var template = File.ReadAllText("VM.template.asm");

            var exec = string.Join(Environment.NewLine, EXEC(ops));

            template = template.Replace("$EXEC$", exec);

            var emit = string.Join(Environment.NewLine, EMIT(ops));

            template = template.Replace("$EMIT$", emit);

            var export = string.Join(",\\" + Environment.NewLine, EXPORT(ops));

            template = template.Replace("$EXPORT$", export);

            File.WriteAllText("VM.asm", template);
        }

        private IEnumerable<string> EXEC(IEnumerable<OP> snips)
        {
            foreach (var snip in snips)
            {
                yield return $"    dq      {snip.Exec}";
            }
            yield return "";

            foreach (var snip in snips)
            {
                yield return $"    {snip.Symbol} = {snip.Number}";
            }
            yield return "";

            foreach (var snip in snips)
            {
                yield return $"{snip.Exec}:";
                foreach (var line in snip.Lines)
                {
                    yield return line;
                }
                yield return "";
            }
        }

        private IEnumerable<string> EMIT(IEnumerable<OP> snips)
        {
            foreach (var snip in snips)
            {
                var register = string.Empty;
                switch (snip.Bytes)
                {
                    case 1:
                        register = "dl";
                        break;
                    case 2:
                        register = "dx";
                        break;
                    case 4:
                        register = "edx";
                        break;
                    case 8:
                        register = "rdx";
                        break;
                }


                yield return $"{snip.Emit}:";
                yield return $"    mov     [rcx], byte {snip.Symbol}";
                if (snip.Bytes > 0)
                {
                    yield return $"    mov     [rcx + 1], {register}";
                }
                yield return $"    lea     rax, [rcx + {snip.Bytes + 1}]";
                yield return $"    ret";
                yield return "";
            }
        }

        private IEnumerable<string> EXPORT(IEnumerable<OP> snips)
        {
            foreach (var snip in snips)
            {
                yield return $"            {snip.Emit}, '{snip.Emit}'";
            }
        }
    }
}
