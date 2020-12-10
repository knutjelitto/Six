namespace SixComp
{
    public partial class ParseTree
    {
        public class AnyType : IType
        {
            public AnyType(Token token)
            {
                Token = token;
            }

            public Token Token { get; }

            public static AnyType Parse(Parser parser)
            {
                var token = parser.Consume(ToKind.KwANY);

                return new AnyType(token);
            }
        }
    }
}
