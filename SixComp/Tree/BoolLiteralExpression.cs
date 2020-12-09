using SixComp.Support;

namespace SixComp
{
    public partial class Tree
    {
        public class BoolLiteralExpression : AnyLiteralExpression
        {
            public static readonly TokenSet Firsts = new TokenSet(ToKind.KwFalse, ToKind.KwTrue);

            public BoolLiteralExpression(Token token) : base(token)
            {
            }

            public static BoolLiteralExpression Parse(Parser parser)
            {
                var token = parser.Consume(Firsts);

                return new BoolLiteralExpression(token);
            }
        }
    }
}