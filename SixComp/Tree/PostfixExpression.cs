namespace SixComp.Tree
{
    public abstract class PostfixExpression : BaseExpression, AnyPostfixExpression
    {
        public PostfixExpression(AnyExpression left, Token op)
        {
            Left = left;
            Op = op;
            Operator = BaseName.From(Op);
        }

        public AnyExpression Left { get; }
        public Token Op { get; }
        public BaseName Operator { get; }

        public override string ToString()
        {
            return $"{Left}{Op}";
        }
    }
}
