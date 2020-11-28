using SixComp.Support;
using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class SwitchCaseClause : ItemList<SwitchCase>
    {
        public SwitchCaseClause(List<SwitchCase> cases) : base(cases) { }
        public SwitchCaseClause() { }

        public static SwitchCaseClause Parse(Parser parser)
        {
            parser.Consume(ToKind.LBrace);

            var cases = new List<SwitchCase>();

            while (parser.Current != ToKind.RBrace)
            {
                CcBlock.Ignore(parser, force: false);
                var @case = SwitchCase.Parse(parser);
                cases.Add(@case);
            }

            parser.Consume(ToKind.RBrace);

            return new SwitchCaseClause(cases);
        }

        public override void Write(IWriter writer)
        {
            using (writer.Block())
            {
                base.Write(writer);
            }
        }

        public override string ToString()
        {
            return $"{{ {string.Join(" ", this)} }}";
        }
    }
}
