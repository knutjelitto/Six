namespace SixComp.ParseTree
{
    public class NumberLiteralExpression : Expression
    {
        public NumberLiteralExpression(Token number)
        {
            Number = number;
        }

        public Token Number { get; }

        public override string ToString()
        {
            return $"(# {Number.Span})";
        }
    }
}
