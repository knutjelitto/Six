using System;

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
            Test("func fib() { return 12 }");
        }

        public void Test(string text)
        {
            var source = new Source("<intern>", text);
            var lex = new Lexer(source);
            Token? token = null;
            while ((token = lex.Next()) != null)
            {
                Console.WriteLine($"{token.Value.Kind}");
            }

            if (!lex.Done)
            {
                Console.WriteLine($"lexer error at >>>{lex.Rest}");
            }
        }
    }
}
