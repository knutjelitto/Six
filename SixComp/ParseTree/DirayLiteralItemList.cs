using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class DirayLiteralItemList : ItemList<DirayLiteralItem>
    {
        public DirayLiteralItemList(List<DirayLiteralItem> items) : base(items) { }
        public DirayLiteralItemList() { }

        public static DirayLiteralItemList Parse(Parser parser)
        {
            var items = new List<DirayLiteralItem>();

            if (parser.Current != ToKind.RBracket)
            {
                do
                {
                    var item = DirayLiteralItem.Parse(parser);

                    items.Add(item);

                    parser.Match(ToKind.Comma);
                }
                while (parser.Current != ToKind.RBracket);
            }

            return new DirayLiteralItemList(items);
        }

        public override string ToString()
        {
            return string.Join(", ", this);
        }
    }
}
