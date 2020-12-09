namespace SixComp
{
    public partial class Tree
    {
        public class ColumnLiteralExpression : AnyLiteralExpression
        {
            private ColumnLiteralExpression(Token token)
                : base(token)
            {
            }

            public static ColumnLiteralExpression Parse(Parser parser)
            {
                var token = parser.Consume(ToKind.CdColumn);

                return new ColumnLiteralExpression(token);
            }
        }
    }
}