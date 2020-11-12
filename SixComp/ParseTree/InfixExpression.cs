using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class InfixExpression : Expression
    {
        public InfixExpression(Expression left, Token op, Expression right)
        {
            Left = left;
            Op = op;
            Right = right;
        }

        public Expression Left { get; }
        public Token Op { get; }
        public Expression Right { get; }

        public override string ToString()
        {
            return $"(_{Op.Span}_ {Left} {Right})";
        }
    }
}
