namespace SixComp.Tree
{
    public class Optional : SyntaxNode
    {
        public Optional(Token? token)
        {
            Token = token;
        }

        public Token? Token { get; }

        public bool Present => Token != null;

        public static Optional Parse(Parser parser, ToKind kind)
        {
            if (parser.Current == kind)
            {
                return new Optional(parser.Consume(kind));
            }
            return new Optional(null);
        }

        public override string ToString()
        {
            return Present ? $" {Token}" : string.Empty;
        }
    }
}
