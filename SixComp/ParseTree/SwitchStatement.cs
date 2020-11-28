using SixComp.Support;
using System;

namespace SixComp.ParseTree
{
    public class SwitchStatement : AnyStatement
    {
        public SwitchStatement(AnyExpression value, SwitchCaseClause cases)
        {
            Value = value;
            Cases = cases;
        }

        public AnyExpression Value { get; }
        public SwitchCaseClause Cases { get; }

        public static SwitchStatement Parse(Parser parser)
        {
            parser.Consume(ToKind.KwSwitch);
            var value = AnyExpression.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(SwitchStatement)}");
            var cases = SwitchCaseClause.Parse(parser);

            return new SwitchStatement(value, cases);
        }

        public void Write(IWriter writer)
        {
            writer.WriteLine($"switch {Value}");
            Cases.Write(writer);
        }

        public override string ToString()
        {
            return $"switch {Value} {Cases}";
        }
    }
}
