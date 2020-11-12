using SixComp.Support;
using System;
using System.IO;

namespace SixComp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new Program().Test();
            Console.Write("(almost) any key ... ");
            Console.ReadKey(true);
        }

        public void Test()
        {
            var source = File.ReadAllText(@"./Samples/Test.six");
            Test(source);
#if false
            Test("11+12?12*13:13-14");
            Test("+++11");
            Test("+11+-12*13");
            Test("func fib(): int { return 12 }");
#endif
        }

        public void Test(string text)
        {
            var source = new Source("<intern>", text);
            var lexer = new Lexer(source);
            while (!lexer.Done)
            {
                var token = lexer.Next(); ;
                //Console.WriteLine($"{token.Kind}");
                if (token.Kind == ToKind.ERROR)
                {
                    break;
                }
            }

            if (!lexer.Done)
            {
                Console.WriteLine($"lexer error at >>>{lexer.Rest}");
            }

            lexer = new Lexer(source);
            var parser = new Parser(lexer);

            try
            {
                Console.WriteLine($"parsing \"\"\"");
                Console.Write(text);
                Console.WriteLine("\"\"\"");

                var tree = parser.Parse();

                if (parser.Ahead(0).Kind != ToKind.EOF)
                {
                    Console.WriteLine($"can't continue parsing at {parser.Ahead(0)}");
                }

                tree.Write(new ConsoleWriter());
            }
            catch (InvalidOperationException error)
            {
                Console.WriteLine($"error: {error.Message}");
            }
        }
    }
}
