namespace SixComp.ParseTree
{
    public class ForcedValueExpression : AnyPostfix
    {
        public ForcedValueExpression(AnyExpression left)
        {
            Left = left;
        }

        public AnyExpression Left { get; }

        public static ForcedValueExpression Parse(Parser parser, AnyExpression left)
        {
            parser.Consume(ToKind.Bang);
            return new ForcedValueExpression(left);
        }
    }
}
