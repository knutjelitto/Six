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

        public Tree.CompilationUnit? Parse(IWriter writer, Context context)
        {
            var parser = context.Parser;

            try
            {
                var tree = parser.Parse();
                return tree;
            }
            catch (ParserException parserError)
            {
                context.Error.Report(writer, parserError.Message, parserError.Token);

                return null;

            }
            catch (LexerException lexerError)
            {
                context.Error.Report(writer, lexerError.Message, lexerError.Offset);

                return null;
            }
            catch (InvalidOperationException invalid)
            {
                Console.WriteLine(invalid.ToString());
                context.Error.Report(writer, $"internal error - {invalid.Message}", parser.CurrentToken);

                return null;
            }
            catch (Exception any)
            {
                Console.WriteLine("FATAL ERROR");
                Console.WriteLine(any.ToString());

                return null;
            }
        }

        private void Error(Context context, string error, Token atToken)
        {
            Error(context.Source, error, atToken.Span.Start, atToken.Span.Length);
        }

        private void Error(Context context, string error, int start, int length = 1)
        {
            Error(context.Source, error, start, length);
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
