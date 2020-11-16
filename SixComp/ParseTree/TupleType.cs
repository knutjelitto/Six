using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class TupleType : ItemList<TupleTypeItem>, AnyType
    {
        public TupleType(List<TupleTypeItem> items) : base(items) { }
        public TupleType() { }

        public static TupleType Parse(Parser parser)
        {
            parser.Consume(ToKind.LParen);

            var items = new List<TupleTypeItem>();
            while (parser.Current.Kind != ToKind.RParent)
            {
                var item = TupleTypeItem.Parse(parser);
                items.Add(item);
                if (parser.Current.Kind != ToKind.RParent)
                {
                    parser.Consume(ToKind.Comma);
                }
            }
            parser.Consume(ToKind.RParent);

            return new TupleType(items);
        }

        public override string ToString()
        {
            return $"({ string.Join(", ", this) })";
        }
    }
}
