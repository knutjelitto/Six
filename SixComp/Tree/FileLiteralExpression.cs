namespace SixComp
{
    public partial class Tree
    {
        public class FileLiteralExpression : AnyLiteralExpression
        {
            private FileLiteralExpression(Token token)
                : base(token)
            {
            }

            public static FileLiteralExpression Parse(Parser parser)
            {
                var token = parser.Consume(ToKind.CdFile);

                return new FileLiteralExpression(token);
            }
        }
    }
}