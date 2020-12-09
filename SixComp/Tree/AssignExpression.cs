using System;

namespace SixComp
{
    public partial class Tree
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
                parser.Consume(ToKind.Assign);

                var right = AnyExpression.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(AssignExpression)}");

                return new AssignExpression(left, right);
            }

            public override string ToString()
            {
                return $"({Left} = {Right})";
            }
        }
    }
}