﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class InOutExpression : AnyExpression
    {
        public InOutExpression(Name name)
        {
            Name = name;
        }

        public Name Name { get; }

        public static InOutExpression Parse(Parser parser)
        {
            parser.Consume(ToKind.Amper);

            var name = Name.Parse(parser);

            return new InOutExpression(name);
        }

        public override string ToString()
        {
            return $"&{Name}";
        }
    }
}
