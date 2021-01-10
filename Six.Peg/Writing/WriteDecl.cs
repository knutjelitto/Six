using Six.Support;
using System;

namespace SixPeg.Writing
{
    public class WriteDecl
    {
        public string NmResult => "match";
        public string NmResults => "matches";
        public string NmOomResults => "oomMatches";
        public string NmZomResults => "zomMatches";
        public string NmNext => "next";
        public string NmOomNext => "oomNext";
        public string NmZomNext => "zomNext";
        public string NmMatchType => "Match";

        public WriteDecl(IWriter writer, Namer n)
        {
            Writer = writer;
            N = n;
        }

        public IWriter Writer { get; }
        public Namer N { get; }

        public void NL()
        {
            Writer.WriteLine();
        }

        public void Line(string text)
        {
            Writer.WriteLine(text);
        }

        public IDisposable Block(string text = null)
        {
            if (text != null)
            {
                Writer.WriteLine(text);
            }
            return Writer.Block();
        }

        public IDisposable If(string text)
        {
            return Block($"if ({text})");
        }

        public IDisposable Else()
        {
            return Block($"else");
        }

        public IDisposable Indent(string comment)
        {
            return Writer.Indent(
                () => Writer.WriteLine($"/*>>> {comment} */{{"),
                () => Writer.WriteLine($"}}/*<<< {comment} */"));
        }

        public string NewVar(string name, string init)
        {
            var local = N.Local(name);
            Line($"var {local} = {init};");
            return local;
        }

        public string NewMatch(string init = null)
        {
            var local = N.Local(NmResult);
            init = init == null ? string.Empty : $" = {init}";
            Line($"{NmMatchType} {local}{init};");
            return local;
        }

        public string NewMatches(string name = null)
        {
            var local = N.Local(name ?? NmResults);
            Line($"var {local} = new List<{NmMatchType}>();");
            return local;
        }
    }
}
