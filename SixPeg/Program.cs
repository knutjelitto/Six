using System;
using System.Collections.Generic;
using System.Text;

namespace SixPeg
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            foreach (var arg in args) { Console.WriteLine($"{arg}"); }

            var engine = new Engine("Swift");

            var grammar = engine.CreateGrammar();


            //var test = ;
            var tests = new List<string>
            {
                //"../../../../Six.Source/Tests/Literals.swift",
                "../../../../Six.Source/Tests/ERROR.swift",
            };
            tests.AddRange(engine.AllTestFiles());

            bool ok = false;

            if (grammar != null && !grammar.Error)
            {
                ok = engine.Test(grammar, tests);
            }

            if (ok)
            {
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("FAIL");
            }
            Console.Write("done ... ");
            _ = Console.ReadKey(true);
        }
    }
}
