using Six.Support;
using SixComp.Support;
using System;

namespace SixComp
{
    public class Compiler
    {
        public Compiler(CompNavi navi)
        {
            Navi = navi;
        }

        public CompNavi Navi { get; }

        public ParseTree.CompilationUnit? Parse(IWriter writer, Context context)
        {
            var parser = context.Parser;

            try
            {
                var tree = parser.Parse();
                return tree;
            }
            catch (ParserException parserError)
            {
                context.Error.Report(writer, parserError.Message, parserError.Token.Span);

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
                context.Error.Report(writer, $"internal error - {invalid.Message}", parser.CurrentToken.Span);

                return null;
            }
            catch (Exception any)
            {
                Console.WriteLine("FATAL ERROR");
                Console.WriteLine(any.ToString());

                return null;
            }
        }
    }
}
