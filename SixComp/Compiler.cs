using SixComp.Support;
using System;

namespace SixComp
{
    public class Compiler
    {
        public Compiler(Navi navi)
        {
            Navi = navi;
        }

        public Navi Navi { get; }

        public Tree.CompilationUnit? Parse(Context context)
        {
            var parser = context.Parser;
            var source = context.Source;

            try
            {
                var tree = parser.Parse();
                return tree;
            }
            catch (ParserException parserError)
            {
                Error(source, parserError.Message, parserError.Token.Span.Start, parserError.Token.Span.Length);

                return null;

            }
            catch (LexerException lexerError)
            {
                Error(source, lexerError.Message, lexerError.Offset, 1);

                return null;
            }
            catch (InvalidOperationException invalid)
            {
                Console.WriteLine(invalid.ToString());
                Error(source, $"internal error - {invalid.Message}", parser.CurrentToken.Span.Start, parser.CurrentToken.Span.Length);

                return null;
            }
            catch (Exception any)
            {
                Console.WriteLine(any.ToString());

                return null;
            }
        }

        private void Error(Source source, string error, int start, int length)
        {
            Console.WriteLine($"error: {error}");
            var info = source.Index.GetInfo(start);
            Console.WriteLine($"    --> {source.Name}[{info.lineNumber},{info.columnNumber}]");
            if (info.lineNumber == 1)
            {
                Console.WriteLine($"     |");
            }
            else
            {
                for (var i = info.lineNumber - 1 - 15; i < info.lineNumber - 1; i += 1)
                {
                    var line = source.Index.GetLine(i);
                    if (line != null)
                    {
                        Console.WriteLine($"     | {line}");
                    }
                }
            }
            Console.WriteLine($"{info.lineNumber,4} | {info.line}");
            var arrow = length > 1 ? $"^{new string('-', length - 2)}^" : "^";
            Console.WriteLine($"     = {new string(' ', info.columnNumber - 1)}{arrow}");
            Console.WriteLine($"     = {new string(' ', info.columnNumber - 1)}`-- {error}");
            Console.WriteLine($"     =");
            for (var i = info.lineNumber; i < info.lineNumber + 3; i += 1)
            {
                var line = source.Index.GetLine(i);
                if (line != null)
                {
                    Console.WriteLine($"     | {line}");
                }
            }
        }

    }
}
