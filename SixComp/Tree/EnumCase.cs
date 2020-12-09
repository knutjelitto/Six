using SixComp.Support;

namespace SixComp
{
    public partial class Tree
    {
        public class EnumCase : AnyDeclaration
        {
            public EnumCase(Prefix prefix, EnumCaseItemList caseItems)
            {
                Prefix = prefix;
                CaseItems = caseItems;
            }

            public Prefix Prefix { get; }
            public EnumCaseItemList CaseItems { get; }

            public static EnumCase Parse(Parser parser, Prefix prefix)
            {
                var caseItems = EnumCaseItemList.Parse(parser);

                return new EnumCase(prefix, caseItems);
            }

            public void Write(IWriter writer)
            {
                foreach (var items in CaseItems)
                {
                    items.Write(writer);
                }
            }

            public override string ToString()
            {
                return $"{CaseItems}";
            }
        }
    }
}