using SixComp.Support;

namespace SixComp.ParseTree
{
    public class ReturnStatement : AnyStatement
    {
        public ReturnStatement(AnyExpression? value)
        {
            Value = value;
        }

        public AnyExpression? Value { get; }

        public static ReturnStatement Parse(Parser parser)
        {
            parser.Consume(ToKind.KwReturn);

            AnyExpression? value = null;

            if (!parser.Current.NewLine)
            {
                value = AnyExpression.Parse(parser);
            }

            return new ReturnStatement(value);
        }

        public void Write(IWriter writer)
        {
            var value = Value == null ? string.Empty : $" {Value.StripParents()}";

            writer.WriteLine($"return{value}");
        }
    }
}
