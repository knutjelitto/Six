using System;

namespace SixComp.ParseTree
{
    public class TernaryExpression : BaseExpression, AnyExpression
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

            var whenTrue = AnyExpression.TryParse(parser, precedence) ?? throw new InvalidOperationException();

            parser.Consume(ToKind.Colon);

            var whenFalse = AnyExpression.TryParse(parser, precedence + 1) ?? throw new InvalidOperationException();

            return new TernaryExpression(condition, whenTrue, whenFalse);
        }

        public override string ToString()
        {
            return $"({Condition} ? {WhenTrue} : {WhenFalse})";
        }
    }
}
