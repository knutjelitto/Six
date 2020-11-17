﻿using SixComp.Support;

namespace SixComp.ParseTree
{
    public class SwitchStatement : AnyStatement
    {
        public SwitchStatement(AnyExpression value, SwitchCaseList cases)
        {
            Value = value;
            Cases = cases;
        }

        public AnyExpression Value { get; }
        public SwitchCaseList Cases { get; }

        public static SwitchStatement Parse(Parser parser)
        {
            parser.Consume(ToKind.KwSwitch);
            var value = AnyExpression.Parse(parser);
            var cases = SwitchCaseList.Parse(parser);

            return new SwitchStatement(value, cases);
        }

        public void Write(IWriter writer)
        {
            writer.WriteLine($"switch {Value}");
            using (writer.Block())
            {
                Cases.Write(writer);
            }
        }
    }
}
