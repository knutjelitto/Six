namespace SixComp
{
    public partial class ParseTree
    {
        public abstract class PostfixExpression : BaseExpression, IPostfixExpression
        {
            public PostfixExpression(IExpression left, Token op)
            {
                Left = left;
                Op = op;
                Operator = BaseName.From(Op);
            }

            public IExpression Left { get; }
            public Token Op { get; }
            public BaseName Operator { get; }

            public override string ToString()
            {
                return $"{Left}{Op}";
            }
        }
    }
}