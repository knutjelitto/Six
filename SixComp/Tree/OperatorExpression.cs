namespace SixComp
{
    public partial class Tree
    {
        public class OperatorExpression : BaseExpression
        {
            public OperatorExpression(Token op)
            {
                Op = op;
                Operator = BaseName.From(Op);
            }

            public Token Op { get; }
            public BaseName Operator { get; }

            public static OperatorExpression Parse(Parser parser)
            {
                var @operator = parser.ConsumeAny();

                return new OperatorExpression(@operator);
            }

            public override string ToString()
            {
                return $"{Op}";
            }
        }
    }
}