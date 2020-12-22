using System;
using System.Collections.Generic;

namespace SixPeg
{
    internal class Program
    {
        internal static void Test(Engine engine, IEnumerable<string> testFiles)
        {
            var grammar = engine.CreateGrammar();

            bool ok = false;

            if (grammar != null && !grammar.Error)
            {
                ok = engine.Test(grammar, testFiles);
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

            var tests = new List<string>
            {
                //"../../../../Six.Source/Swift/Tests/Literals.swift",
                "../../../../Six.Source/Swift/Tests/ERROR.swift",
            };
            tests.AddRange(engine.AllTestFiles());

            Test(engine, tests);
        }


        internal static void TestGo()
        {
            var engine = new Engine("Go");

            var tests = new List<string>
            {
                "../../../../Six.Source/Go/Tests/ERROR.go",
            };
            tests.AddRange(engine.ManyGoFiles());

            Test(engine, tests);
        }


        internal static void Main(string[] args)
        {
            foreach (var arg in args) { Console.WriteLine($"{arg}"); }

            TestGo();

            Console.Write("done ... ");
            _ = Console.ReadKey(true);
        }
    }
}
