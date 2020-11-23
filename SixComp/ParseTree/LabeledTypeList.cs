using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class LabeledTypeList : ItemList<TupleTypeElement>, AnyType
    {
        public LabeledTypeList(List<TupleTypeElement> items) : base(items) { }
        public LabeledTypeList() { }

        public static LabeledTypeList Parse(Parser parser)
        {
            parser.Consume(ToKind.LParent);

            var items = new List<TupleTypeElement>();
            if (parser.Current != ToKind.RParent)
            {
                do
                {
                    var item = TupleTypeElement.Parse(parser);
                    items.Add(item);
                }
                while (parser.Match(ToKind.Comma));
            }
            parser.Consume(ToKind.RParent);

            return new LabeledTypeList(items);
        }

        public override string ToString()
        {
            return string.Join(", ", this);
        }
    }
}
