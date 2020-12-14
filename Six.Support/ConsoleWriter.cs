using System;

namespace Six.Support
{
    public class ConsoleWriter : IndentWriter
    {
        public ConsoleWriter() : base(new BaseTextWriter(Console.Out)) { }
    }
}
