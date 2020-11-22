using System;

namespace SixComp.ParseTree
{
    public class AssignExpression : BaseExpression, AnyExpression
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

            var right = AnyExpression.TryParse(parser) ?? throw new InvalidOperationException();

            return new AssignExpression(left, right);
        }

        public override string ToString()
        {
            return $"({Left} = {Right})";
        }
    }
}
