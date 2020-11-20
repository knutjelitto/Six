using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.Support
{
    public class ParserException : Exception
    {
        public ParserException(Token token, string message) : base(message)
        {
            Token = token;
        }

        public Token Token { get; }
    }
}
