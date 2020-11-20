using System;

namespace SixComp.Support
{
    public class LexerException : Exception
    {
        public LexerException(int offset, string message) : base(message)
        {
            Offset = offset;
        }

        public int Offset { get; }
    }
}
