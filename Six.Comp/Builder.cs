using Six.Support;
using Six.Peg.Runtime;
using System;
using System.Collections.Generic;

namespace Six.Comp
{
    public abstract class Builder
    {
        protected Builder(Navi navi)
        {
            Navi = navi;
        }

        public Navi Navi { get; }

        protected abstract List<SourceFile> Files();

        public virtual bool Build()
        {
            try
            {
                var sources = Files();
                foreach (var source in Files())
                {
                    var skip = source.Skip ? " SKIP" : string.Empty;

                    Console.WriteLine($"[{source.No}/{sources.Count}]{skip} {source.Name}");

                    Compile(source);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected virtual void Compile(SourceFile source)
        {
            var context = new Context(source.Path.FullName);
            var parser = new Parser(context);

            var match = parser.Unit(0);
            if (match != null)
            {
                DumpTree(source, match);
            }
        }


        protected virtual void DumpTree(SourceFile source, Match match)
        {
            var parseTreeFile = Navi.File(Navi.TempFor(source.Path.Directory), source.Path.Name + ".tree");
            Console.WriteLine($"  -> {parseTreeFile}");

            using var writer = new FileWriter(parseTreeFile.FullName);
            DumpTree(writer, match);
        }

        protected virtual void DumpTree(IWriter writer, Match match)
        {
            var count = match.Matches.Count > 0 ? $" [{match.Matches.Count}]" : string.Empty;
            var length = $" #{match.Lenght}";
            using (writer.Indent($"{match.Name}{count}{length}"))
            {
                foreach (var submatch in match.Matches)
                {
                    DumpTree(writer, submatch);
                }
            }
        }
    }
}
