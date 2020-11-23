namespace SixComp.ParseTree
{
    public class BinaryExpression: SyntaxNode
    {
        public BinaryExpression(Token @operator, AnyExpression right)
        {
            Operator = @operator;
            Right = right;
        }

        public Token Operator { get; }
        public AnyExpression Right { get; }

        public static BinaryExpression? TryParse(Parser parser)
        {
            if (parser.IsInfix())
            {
                var offset = parser.Offset;

                var op = parser.ConsumeAny();
                var right = AnyPrefix.TryParse(parser);

                if (right != null)
                {
                    return new BinaryExpression(op, right);
                }
                parser.Offset = offset;
            }

            return null;
        }
    }
}
