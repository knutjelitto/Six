using SixComp.Support;
using System;

namespace SixComp.ParseTree
{
    public class LabeledStatement : AnyStatement
    {
        public LabeledStatement(Name label, AnyStatement statement)
        {
            Label = label;
            Statement = statement;
        }

        public Name Label { get; }
        public AnyStatement Statement { get; }

        public static LabeledStatement Parse(Parser parser)
        {
            var label = Name.Parse(parser);
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
