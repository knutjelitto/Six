using SixComp.Support;
using System;

namespace SixComp.ParseTree
{
    public class EnumCase : AnyDeclaration
    {
        public EnumCase(EnumCaseItemList caseItems)
        {
            CaseItems = caseItems;
        }

        public EnumCaseItemList CaseItems { get; }

        public static EnumCase Parse(Parser parser)
        {
            // already parsed //parser.Consume(ToKind.KwCase);

            var caseItems = EnumCaseItemList.Parse(parser);

            return new EnumCase(caseItems);
        }

        public void Write(IWriter writer)
        {
            foreach (var items in CaseItems)
            {
                items.Write(writer);
            }
        }
    }
}
