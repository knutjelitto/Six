using SixComp.Support;
using System;

namespace SixComp
{
    public partial class Tree
    {
        public class LabeledStatement : AnyStatement
        {
            public LabeledStatement(BaseName label, AnyStatement statement)
            {
                Label = label;
                Statement = statement;
            }

            public BaseName Label { get; }
            public AnyStatement Statement { get; }

            public static LabeledStatement Parse(Parser parser)
            {
                var label = BaseName.Parse(parser);
                parser.Consume(ToKind.Colon);
                var statement = AnyStatement.TryParse(parser) ?? throw new InvalidOperationException($"{typeof(LabeledStatement)}");
                return new LabeledStatement(label, statement);
            }

            public void Write(IWriter writer)
            {
                writer.WriteLine($"{Label}:");
                using (writer.Indent())
                {
                    Statement.Write(writer);
                }
            }

            public override string ToString()
            {
                return $"{Label}: {Statement}";
            }
        }
    }
}