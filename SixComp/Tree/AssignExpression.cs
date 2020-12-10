using System;

namespace SixComp
{
    public partial class ParseTree
    {
        public class AssignExpression : BaseExpression, IExpression
        {
            public AssignExpression(IExpression left, IExpression right)
            {
                Left = left;
                Right = right;
            }

            public IExpression Left { get; }
            public IExpression Right { get; }

            public static AssignExpression Parse(Parser parser, IExpression left)
            {
                parser.Consume(ToKind.Assign);

                var right = IExpression.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(AssignExpression)}");

                return new AssignExpression(left, right);
            }

            public override string ToString()
            {
                return $"({Left} = {Right})";
            }
        }
    }
}