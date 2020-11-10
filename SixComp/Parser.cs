using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp
{
    public class Parser
    {
        public Parser(Lexer lexer)
        {
            Lexer = lexer;
        }

        public Lexer Lexer { get; }

        public void Parse()
        {

        }
    }
}
