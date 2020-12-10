using SixComp.Support;
using System.Collections.Generic;

namespace SixComp
{
    public partial class ParseTree
    {
        public class TuplePatternElementList : ItemList<TuplePatternElement>
        {
            public TuplePatternElementList(List<TuplePatternElement> elements) : base(elements) { }
            public TuplePatternElementList() { }

            public static TuplePatternElementList Parse(Parser parser, TokenSet follows)
            {
                var elements = new List<TuplePatternElement>();

                if (!follows.Contains(parser.Current))
                {
                    do
                    {
                        var element = TuplePatternElement.Parse(parser);
                        elements.Add(element);
                    }
                    while (parser.Match(ToKind.Comma));
                }

                return new TuplePatternElementList(elements);
            }

            public override string ToString()
            {
                return string.Join(", ", this);
            }
        }
    }
}