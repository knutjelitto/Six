using SixComp.Support;
using System;

namespace SixComp
{
    public partial class ParseTree
    {
        public class SwitchStatement : IStatement
        {
            public SwitchStatement(IExpression value, SwitchCaseClause cases)
            {
                Value = value;
                Cases = cases;
            }

            public IExpression Value { get; }
            public SwitchCaseClause Cases { get; }

            public static SwitchStatement Parse(Parser parser)
            {
                parser.Consume(ToKind.KwSwitch);
                var value = IExpression.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(SwitchStatement)}");
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
}