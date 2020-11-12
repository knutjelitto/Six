using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class CallExpression : Expression
    {
        public CallExpression(Expression left, List<Expression> arguments)
        {
            Left = left;
            Arguments = arguments;
        }

        public Expression Left { get; }
        public List<Expression> Arguments { get; }

        public override string ToString()
        {
            var args = string.Join(" ", Arguments);

            return $"(call {Left} {args})";
        }
    }
}
