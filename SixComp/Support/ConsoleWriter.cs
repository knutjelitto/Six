using System;

namespace SixComp.Support
{
    public class ConsoleWriter : IndentWriter
    {
        public ConsoleWriter() : base(new BaseTextWriter(Console.Out)) { }
    }
}
