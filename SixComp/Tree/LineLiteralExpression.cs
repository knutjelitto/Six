namespace SixComp
{
    public partial class Tree
    {
        public class LineLiteralExpression : AnyLiteralExpression
        {
            private LineLiteralExpression(Token token)
                : base(token)
            {
            }

            public static LineLiteralExpression Parse(Parser parser)
            {
                var token = parser.Consume(ToKind.CdLine);

                return new LineLiteralExpression(token);
            }
        }
    }
}