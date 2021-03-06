﻿using Six.Peg.Runtime;
using Six.Support;
using SixPeg.Matchers;
using SixPeg.Visiting;
using SixPeg.Writing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SixPeg
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            foreach (var arg in args) { Console.WriteLine($"{arg}"); }

            //TestLang("Swift");

            var builder = new Builder();
            builder.Build();

            Console.Write("done ... ");
            _ = Console.ReadKey(true);
        }

        internal static Parser Test(Engine engine, List<TestFile> testFiles)
        {
            var ok = false;

            try
            {
                var grammar = engine.CreateGrammar();
                var parser = engine.CreateParser(grammar);

                if (grammar != null && !grammar.Error)
                {
                    ok = engine.Test(parser, testFiles);
                }

                return parser;
            }
            catch (BailOutException)
            {
            }

            if (ok)
            {
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("FAIL");
            }

            return null;
        }

        internal static void Test(Engine engine, Parser parser, List<TestFile> testFiles)
        {
            var ok = false;

            if (parser != null)
            {
                try
                {
                    ok = engine.Test(parser, testFiles);
                }
                catch (BailOutException)
                {
                }
            }

            if (ok)
            {
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("FAIL");
            }
        }

        internal static void TestSwift()
        {
            var engine = new Engine("Swift");

            var tests = new List<TestFile>
            {
                new TestFile("../../../../Six.Source/Swift/Tests/Literals.swift", "Tests/Literals.swift", 1, true),
                new TestFile("../../../../Six.Source/Swift/Tests/ERROR.swift", "Tests/ERROR.swift", 2, false),
            };
            tests.AddRange(engine.ManySwiftFiles(tests.Count));

            _ = Test(engine, tests);
        }


        internal static void TestLang(string language)
        {
            var engine = new Engine(language);

            var files = new List<TestFile>
            {
                new TestFile($"../../../../Six.Source/{language}/Tests/ERROR.{language.ToLower()}", $"ERROR.{language.ToLower()}", 1, false),
            };
            files.AddRange(engine.ManyTestFiles(language, files.Count));

            var temp = engine.Navi.TempFor(new DirectoryInfo(Path.Combine(engine.Navi.Project.FullName, language)));

            var parser = engine.CreateParser();

            using (var writer = new FileWriter(Path.Combine(temp.FullName, "parser.txt")))
            {
                new PrintVisitor(writer).Print(parser);
            }

            using (var writer = new FileWriter(Path.Combine(temp.FullName, "Pegger.cs")))
            {
                new Emitter(parser, writer).Emit();
            }

            Test(engine, parser, files);

            using (var writer = new FileWriter(Path.Combine(temp.FullName, "stats.txt")))
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
                foreach (var file in files)
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
        }
    }
}
