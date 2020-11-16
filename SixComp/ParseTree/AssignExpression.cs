using System;
using System.Collections.Generic;
using System.Text;

namespace SixComp.ParseTree
{
    public class AssignExpression : AnyExpression
    {
        public AssignExpression(AnyExpression left, AnyExpression right)
        {
            Left = left;
            Right = right;
        }

        public AnyExpression Left { get; }
        public AnyExpression Right { get; }

        public static AssignExpression Parse(Parser parser, AnyExpression left)
        {
            parser.Consume(ToKind.Equal);

            var right = AnyExpression.Parse(parser);

            return new AssignExpression(left, right);
        }

        public override string ToString()
        {
            return $"({Left} = {Right})";
        }
    }
}
