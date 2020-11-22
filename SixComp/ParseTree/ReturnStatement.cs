using SixComp.Support;
using System;

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

            if (!parser.CurrentToken.NewlineBefore)
            {
                value = AnyExpression.TryParse(parser) ?? throw new InvalidOperationException();
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
