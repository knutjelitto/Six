using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class SwitchCaseList : ItemList<SwitchCase>
    {
        public SwitchCaseList(List<SwitchCase> cases) : base(cases) { }
        public SwitchCaseList() { }

        public static SwitchCaseList Parse(Parser parser)
        {
            parser.Consume(ToKind.LBrace);

            var cases = new List<SwitchCase>();

            while (parser.Current.Kind != ToKind.RBrace)
            {
                var @case = SwitchCase.Parse(parser);
                cases.Add(@case);
            }

            parser.Consume(ToKind.RBrace);

            return new SwitchCaseList(cases);
        }
    }
}
