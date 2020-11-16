namespace SixComp.ParseTree
{
    public class SelfExpression : AnyPrimary
    {
        public SelfExpression()
        {
        }

        public static SelfExpression Parse(Parser parser)
        {
            parser.Consume(ToKind.KwSelf);

            return new SelfExpression();
        }

        public override string ToString()
        {
            return "self";
        }
    }
}
