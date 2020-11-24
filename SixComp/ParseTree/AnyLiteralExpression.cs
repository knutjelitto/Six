namespace SixComp.ParseTree
{
    public abstract class AnyLiteralExpression : BaseExpression, AnyPrimaryExpression
    {
        public AnyLiteralExpression(Token token)
        {
            Token = token;
        }

        public Token Token { get; }

        public override string ToString()
        {
            return $"{Token}";
        }
    }
}
