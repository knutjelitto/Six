namespace SixComp.ParseTree
{
    public class Name
    {
        public Name(Token token)
        {
            Token = token;
        }

        public Token Token { get; }

        public override string ToString()
        {
            return Token.Span.ToString();
        }
    }
}
