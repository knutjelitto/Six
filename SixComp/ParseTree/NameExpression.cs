using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class NameExpression : Expression
    {
        public NameExpression(Token name)
        {
            Name = name;
        }

        public Token Name { get; }

        public override string ToString()
        {
            return $"(id {Name.Span})";
        }
    }
}
