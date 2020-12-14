using Pegasus.Common;
using Six.Support;
using System;
using System.IO;

namespace SixPeg
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            bool ok = false;

            foreach (var arg in args)
            {
                Console.WriteLine($"{arg}");
            }

            var parser = new Parser.SixParser();

            var file = "SwiftExpression.sixpeg";

            var text = File.ReadAllText(file);

            try
            {
                var result = parser.Parse(text, file);

                using var writer = new FileWriter("../../../../Six.dump");

                result.Resolve(writer);

                writer.WriteLine();
                result.ReportMatchers(writer);

                if (!result.Error)
                {
                    var subject = "1 + 2 * 3 + 1";
                    var cursor = 0;

                    ok = result.Matcher.Match(subject, ref cursor);
                }
                else
                {

                }

                //result.Write(writer);
            }
            catch (FormatException ex)
            {
                var cursor = (Cursor)ex.Data["cursor"];

                Console.WriteLine($"{cursor.FileName}[{cursor.Line},{cursor.Column}]: {ex.Message}");
                Console.WriteLine();
                Console.WriteLine(ex);
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
