﻿using System.Diagnostics;

namespace SixComp.ParseTree
{
    public class ImportPathIdentifier
    {
        public ImportPathIdentifier(Token token)
        {
            Token = token;
        }

        public Token Token { get; }

        public static ImportPathIdentifier Parse(Parser parser)
        {
            Debug.Assert(parser.Current == ToKind.Name || parser.CurrentToken.IsOperator);

            var token = parser.ConsumeAny();

            return new ImportPathIdentifier(token);
        }

        public override string ToString()
        {
            return $"{Token}";
        }
    }
}
