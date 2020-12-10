using SixComp.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public class ReturnStatement : IStatement
        {
            public ReturnStatement(IExpression? value)
            {
                Value = value;
            }

            public IExpression? Value { get; }

            public static ReturnStatement Parse(Parser parser)
            {
                parser.Consume(ToKind.KwReturn);

                IExpression? value = null;

                if (!parser.CurrentToken.NewlineBefore)
                {
                    value = IExpression.TryParse(parser);
                }

                return new ReturnStatement(value);
            }

            public void Write(IWriter writer)
            {
                var value = Value == null ? string.Empty : $" {Value.StripParents()}";

                writer.WriteLine($"return{value}");
            }

            public override string ToString()
            {
                var value = Value == null ? string.Empty : $" {Value}";
                return $"return{value}";
            }
        }
    }
}