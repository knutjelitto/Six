using SixComp.Support;

namespace SixComp.ParseTree
{
    public class YieldStatement : AnyStatement
    {
        public YieldStatement(AnyExpression? value)
        {
            Value = value;
        }

        public AnyExpression? Value { get; }

        public static YieldStatement Parse(Parser parser)
        {
            parser.Consume(ToKind.KwYield);

            AnyExpression? value = null;

            if (!parser.CurrentToken.NewlineBefore)
            {
                value = AnyExpression.TryParse(parser);
            }

            return new YieldStatement(value);
        }

        public void Write(IWriter writer)
        {
            var value = Value == null ? string.Empty : $" {Value.StripParents()}";

            writer.WriteLine($"{ToKind.KwYield.GetRep()}{value}");
        }

        public override string ToString()
        {
            var value = Value == null ? string.Empty : $" {Value}";
            return $"{ToKind.KwYield.GetRep()}{value}";
        }
    }
}
