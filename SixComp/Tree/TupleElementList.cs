using System.Collections.Generic;

namespace SixComp.Tree
{
    public class TupleElementList : ItemList<TupleElement>
    {
        public TupleElementList(List<TupleElement> elements) : base(elements) { }
        public TupleElementList() { }

        public static TupleElementList Parse(Parser parser)
        {
            var elements = new List<TupleElement>();

            if (parser.Current != ToKind.RParent)
            {
                do
                {
                    var element = TupleElement.Parse(parser);
                    elements.Add(element);
                }
                while (parser.Match(ToKind.Comma));
            }

            return new TupleElementList(elements);
        }

        public override string ToString()
        {
            return string.Join(", ", this);
        }
    }
}
