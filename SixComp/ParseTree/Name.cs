namespace SixComp.ParseTree
{
    public class Name
    {
        public Name(Token token)
        {
            Token = token;
        }

        public Token Token { get; }

        public static Name Parse(Parser parser)
        {
            return new Name(parser.Consume(ToKind.Name));
        }

        public override string ToString()
        {
            return Token.Span.ToString();
        }
    }
}
