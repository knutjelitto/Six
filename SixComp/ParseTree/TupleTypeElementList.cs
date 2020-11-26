using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class TupleTypeElementList : ItemList<TupleTypeElement>
    {
        public TupleTypeElementList(List<TupleTypeElement> elements) : base(elements) { }
        public TupleTypeElementList() { }

        public static TupleTypeElementList Parse(Parser parser)
        {
            var elements = new List<TupleTypeElement>();

            if (parser.Current != ToKind.RParent)
            {
                do
                {
                    var argument = TupleTypeElement.Parse(parser);
                    elements.Add(argument);
                }
                while (parser.Match(ToKind.Comma));
            }

            return new TupleTypeElementList(elements);
        }

        public override string ToString()
        {
            return string.Join(", ", this);
        }
    }
}
