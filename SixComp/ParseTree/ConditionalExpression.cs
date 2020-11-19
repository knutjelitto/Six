namespace SixComp.ParseTree
{
    public class ConditionalExpression : AnyExpression
    {
        public ConditionalExpression(AnyExpression condition, AnyExpression ifTrue, AnyExpression ifFalse)
        {
            Condition = condition;
            IfTrue = ifTrue;
            IfFalse = ifFalse;
        }

        public AnyExpression Condition { get; }
        public AnyExpression IfTrue { get; }
        public AnyExpression IfFalse { get; }

        public static AnyExpression Parse(Parser parser, AnyExpression condition, int precedence)
        {
            var token = parser.Consume(ToKind.Quest);

            var offset = parser.Offset;

            var expr1 = AnyExpression.Parse(parser, precedence);

            if (parser.Match(ToKind.Colon))
            {
                var expr2 = AnyExpression.Parse(parser, precedence-1);

                return new ConditionalExpression(condition, expr1, expr2);
            }

            parser.Offset = offset;

            return OptionalChainingExpression.From(condition);
        }

        public override string ToString()
        {
            return $"({Condition} ? {IfTrue} : {IfFalse})";
        }
    }
}
