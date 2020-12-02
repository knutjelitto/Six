using SixComp.Support;

namespace SixComp.Tree
{
    public class SwitchCase : IWritable
    {
        public SwitchCase(CaseLabel label, StatementList statements)
        {
            Label = label;
            Statements = statements;
        }

        public CaseLabel Label { get; }
        public StatementList Statements { get; }

        public static SwitchCase Parse(Parser parser)
        {
            var label = CaseLabel.Parse(parser);
            var statements = StatementList.Parse(parser, new TokenSet(ToKind.KwCase, ToKind.KwDefault, ToKind.RBrace));

            return new SwitchCase(label, statements);
        }

        public void Write(IWriter writer)
        {
            writer.WriteLine($"{Label}");
            using (writer.Indent())
            {
                Statements.Write(writer);
            }
        }

        public override string ToString()
        {
            return $"{Label} {Statements}";
        }
    }
}
