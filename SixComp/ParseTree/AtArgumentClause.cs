﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class AtArgumentClause
    {
        public AtArgumentClause(AtTokenGroup arguments)
        {
            Arguments = arguments;
        }

        public AtTokenGroup Arguments { get; }

        public static AtArgumentClause Parse(Parser parser)
        {
            var arguments = AtTokenGroup.Parse(parser, ToKind.LParent, ToKind.RParent);

            return new AtArgumentClause(arguments);
        }
    }
}
