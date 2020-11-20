namespace SixComp.ParseTree
{
    public class TernaryExpression : AnyExpression
    {
        public TernaryExpression(AnyExpression condition, AnyExpression whenTrue, AnyExpression whenFalse)
        {
            Condition = condition;
            WhenTrue = whenTrue;
            WhenFalse = whenFalse;
        }

        public AnyExpression Condition { get; }
        public AnyExpression WhenTrue { get; }
        public AnyExpression WhenFalse { get; }

        public static TernaryExpression Parse(Parser parser, AnyExpression condition, int precedence)
        {
            parser.Consume(ToKind.Quest);

            var whenTrue = AnyExpression.Parse(parser, precedence);

            parser.Consume(ToKind.Colon);

            var whenFalse = AnyExpression.Parse(parser, precedence + 1);

            return new TernaryExpression(condition, whenTrue, whenFalse);
        }

        public override string ToString()
        {
            return $"({Condition} ? {WhenTrue} : {WhenFalse})";
        }
    }
}
