using SixComp.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public class YieldStatement : IStatement
        {
            public YieldStatement(IExpression? value)
            {
                Value = value;
            }

            public IExpression? Value { get; }

            public static YieldStatement Parse(Parser parser)
            {
                parser.Consume(ToKind.KwYield);

                IExpression? value = null;

                if (!parser.CurrentToken.NewlineBefore)
                {
                    value = IExpression.TryParse(parser);
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
}