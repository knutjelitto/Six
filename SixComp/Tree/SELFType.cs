namespace SixComp
{
    public partial class Tree
    {
        public class SELFType : AnyType
        {
            public SELFType(Token token)
            {
                Token = token;
            }

            public Token Token { get; }

            public static SELFType Parse(Parser parser)
            {
                var token = parser.Consume(ToKind.KwSELF);

                return new SELFType(token);
            }
        }
    }
}