using System;

namespace SixComp.ParseTree
{
    public class DirayLiteralItem : BaseExpression, AnyExpression
    {
        public DirayLiteralItem(AnyExpression? left, AnyExpression? right)
        {
            Left = left;
            Right = right;
        }

        public AnyExpression? Left { get; }
        public AnyExpression? Right { get; }

        public static DirayLiteralItem Parse(Parser parser)
        {
            var left = AnyExpression.TryParse(parser);
            AnyExpression? right = null;
            if (parser.Match(ToKind.Colon))
            {
                right = AnyExpression.TryParse(parser);
            }

            return new DirayLiteralItem(left, right);
        }

        public override string ToString()
        {
            return $"{Left}";
        }
    }
}
