using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class CaseItemList : ItemList<CaseItem>
    {
        public CaseItemList(List<CaseItem> items) : base(items) { }
        public CaseItemList() { }

        public bool IsDefault => Missing;

        public static CaseItemList Parse(Parser parser)
        {
            var items = new List<CaseItem>();

            do
            {
                var item = CaseItem.Parse(parser);
                items.Add(item);
            }
            while (parser.Match(ToKind.Comma));

            return new CaseItemList(items);
        }
    }
}
