using SixComp.Support;

namespace SixComp.ParseTree
{
    public class TuplePattern : SyntaxNode, AnyPattern
    {
        private TuplePattern(TuplePatternElementList elements)
        {
            Elements = elements;
        }

        public TuplePatternElementList Elements { get; }

        public static TuplePattern Parse(Parser parser)
        {
            parser.Consume(ToKind.LParent);

            var elements = TuplePatternElementList.Parse(parser, new TokenSet(ToKind.RParent));

            parser.Consume(ToKind.RParent);

            return new TuplePattern(elements);
        }

        public override string ToString()
        {
            return $"({Elements})";
        }
    }
}
