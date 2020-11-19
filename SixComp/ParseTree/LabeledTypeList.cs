using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class LabeledTypeList : ItemList<LabeledType>, AnyType
    {
        public LabeledTypeList(List<LabeledType> items) : base(items) { }
        public LabeledTypeList() { }

        public static LabeledTypeList Parse(Parser parser)
        {
            var items = new List<LabeledType>();
            while (parser.Current != ToKind.RParent)
            {
                var item = LabeledType.Parse(parser);
                items.Add(item);
                if (parser.Current != ToKind.RParent)
                {
                    parser.Consume(ToKind.Comma);
                }
            }

            return new LabeledTypeList(items);
        }

        public override string ToString()
        {
            return string.Join(", ", this);
        }
    }
}
