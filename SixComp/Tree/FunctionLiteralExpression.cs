namespace SixComp
{
    public partial class Tree
    {
        public class FunctionLiteralExpression : AnyLiteralExpression
        {
            private FunctionLiteralExpression(Token token)
                : base(token)
            {
            }

            public static FunctionLiteralExpression Parse(Parser parser)
            {
                var token = parser.Consume(ToKind.CdFunction);

                return new FunctionLiteralExpression(token);
            }
        }
    }
}