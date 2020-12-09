using SixComp.Support;
using System.Collections.Generic;

namespace SixComp
{
    public partial class Tree
    {
        public class CaseItemList : ItemList<CaseItem>
        {
            private static readonly TokenSet itemFollows = new TokenSet(ToKind.Comma, ToKind.Colon);

            public CaseItemList(List<CaseItem> items) : base(items) { }
            public CaseItemList() { }

            public bool IsDefault => Missing;

            public static CaseItemList Parse(Parser parser)
            {
                var items = new List<CaseItem>();

                do
                {
                    var item = CaseItem.Parse(parser, itemFollows);
                    items.Add(item);
                }
                while (parser.Match(ToKind.Comma));

                return new CaseItemList(items);
            }

            public override string ToString()
            {
                return string.Join(", ", this);
            }
        }
    }
}