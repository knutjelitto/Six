﻿namespace SixComp.ParseTree
{
    public class ANIType : AnyType
    {
        public ANIType(Token token)
        {
            Token = token;
        }

        public Token Token { get; }

        public static ANIType Parse(Parser parser)
        {
            var token = parser.Consume(ToKind.KwANY);

            return new ANIType(token);
        }
    }
}
