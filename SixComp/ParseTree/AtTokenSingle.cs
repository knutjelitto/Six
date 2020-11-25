using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class AtTokenSingle : AtToken
    {
        public AtTokenSingle(Token token)
        {
            Token = token;
        }

        public Token Token { get; }

        public static AtTokenSingle Parse(Parser parser)
        {
            var token = parser.ConsumeAny();

            return new AtTokenSingle(token);
        }

        public override string ToString()
        {
            return $"{Token}";
        }
    }
}
