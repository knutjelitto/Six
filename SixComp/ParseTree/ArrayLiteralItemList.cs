using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class ArrayLiteralItemList : ItemList<ArrayLiteralItem>
    {
        public ArrayLiteralItemList(List<ArrayLiteralItem> items) : base(items) { }
        public ArrayLiteralItemList() { }

        public static ArrayLiteralItemList Parse(Parser parser)
        {
            var items = new List<ArrayLiteralItem>();

            if (parser.Current.Kind != ToKind.RBracket)
            {
                do
                {
                    var item = ArrayLiteralItem.Parse(parser);

                    items.Add(item);

                    parser.Match(ToKind.Comma);
                }
                while (parser.Current.Kind != ToKind.RBracket);
            }

            return new ArrayLiteralItemList(items);
        }

        public override string ToString()
        {
            return string.Join(", ", this);
        }
    }
}
