using Six.Support;
using Six.Peg.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

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

                // Warmup
                foreach (var source in sources.Where(s => !s.Skip).Skip(10).Take(10))
                {
                    Compile(source, true);
                }


                foreach (var source in sources)
                {
                    var skip = source.Skip ? " SKIP" : string.Empty;

                    Console.WriteLine($"[{source.No}/{sources.Count}]{skip} {source.Name}");

                    if (source.Skip)
                    {
                        continue;
                    }

                    Compile(source);
                }

                using (var writer = new FileWriter(Path.Combine(Navi.TempFor(Navi.Project).FullName, "statistics.txt")))
                {
                    Statistics(writer, sources);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected virtual void Compile(SourceFile source, bool silent = false)
        {
            var context = new Context(source.Path.FullName);
            var parser = new Parser(context);

            source.Lines = context.Source.Index.Index.Count;

            var watch = new Stopwatch();
            watch.Start();
            var match = parser.Unit(0);
            watch.Stop();
            source.Time = watch.Elapsed;

#if true
            if (!silent && match != null)
            {
                //DumpTree(context, source, match);
                Rewrite(context, source, match);
            }
#endif
        }


        private void Statistics(IWriter writer, List<SourceFile> files)
        {
            var lines = files.Where(f => !f.Skip).Sum(f => f.Lines);
            var time = TimeSpan.FromSeconds(files.Where(f => !f.Skip).Sum(f => f.Time.TotalSeconds));
            var lps = Math.Round(lines / time.TotalSeconds);

            using (writer.Indent("total:"))
            {
                writer.WriteLine($"lines: {lines}");
                writer.WriteLine($"time : {time}");
                writer.WriteLine($"lps  : {lps}");
            }
            writer.WriteLine();

            var lames = files.OrderBy(f => f.Lps).Take(20);
            foreach (var file in lames)
            {
                writer.WriteLine($"[{file.Lps,5} lps] {file.Name}");
            }

            writer.WriteLine();
            foreach (var file in files.OrderByDescending(f => f.Lps))
            {
                writer.WriteLine($"{file.Name}:");
                using (writer.Indent())
                {
                    writer.WriteLine($"no.  : {file.No}");
                    writer.WriteLine($"skip : {file.Skip}");
                    writer.WriteLine($"lines: {file.Lines}");
                    writer.WriteLine($"time : {file.Time}");
                    writer.WriteLine($"lps  : {file.Lps}");
                }
            }
        }

        protected virtual void Rewrite(Context context, SourceFile source, Match match)
        {
            var outPath = Path.Combine(
                source.Path.Directory.FullName,
                Path.GetFileNameWithoutExtension(source.Path.Name) + ".six");

            using (var writer = new FileWriter(outPath))
            {
                Rewrite(context, writer, match);
            }
        }

        protected virtual void Rewrite(Context context, IWriter writer, Match match)
        {
            if (match.Before < match.Start)
            {
                var space = context.Text[match.Before..match.Start];
                writer.Write(space);
            }
            if (match.Matches.Count == 0)
            {
                var text = context.Text.Substring(match.Start, match.Next - match.Start);
                writer.Write(text);
            }
            else
            {
                foreach (var submatch in match.Matches)
                {
                    Rewrite(context, writer, submatch);
                }
            }
        }

        protected virtual void DumpTree(Context context, SourceFile source, Match match)
        {
            var parseTreeFile = Navi.File(Navi.TempFor(source.Path.Directory), source.Path.Name + ".tree");

            using (var writer = new FileWriter(parseTreeFile.FullName))
            {
                DumpTree(context, writer, match);
            }
        }

        protected virtual void DumpTree(Context context, IWriter writer, Match match)
        {
            var count = match.Matches.Count > 0 ? $" [{match.Matches.Count}]" : string.Empty;
            var before = match.Before < match.Start ? $" <#{match.Start - match.Before}>" : string.Empty;
            var length = $" #{match.Lenght}";

            var content = match.Name switch
            {
                "Name" => Content(),
                _ => string.Empty,
            };

            using (writer.Indent($"{match.Name}{count}{before}{length}{content}"))
            {
                foreach (var submatch in match.Matches)
                {
                    DumpTree(context, writer, submatch);
                }
            }

            string Content()
            {
                return $" `{context.Text.Substring(match.Start, match.Lenght)}´";
            }
        }
    }
}
