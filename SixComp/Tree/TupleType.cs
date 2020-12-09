namespace SixComp
{
    public partial class Tree
    {
        public class TupleType : AnyType
        {
            private TupleType(Prefix prefix, TupleTypeElementList elements)
            {
                Prefix = prefix;
                Elements = elements;
            }

            public Prefix Prefix { get; }
            public TupleTypeElementList Elements { get; }

            public static TupleType Parse(Parser parser)
            {
                parser.Consume(ToKind.LParent);
                var elements = TupleTypeElementList.Parse(parser);
                parser.Consume(ToKind.RParent);

                return new TupleType(Prefix.Empty, elements);
            }

            public static TupleType From(Prefix prefix, TupleTypeElementList elements)
            {
                return new TupleType(prefix, elements);
            }

            public override string ToString()
            {
                return $"{Prefix}({Elements})";
            }
        }
    }
}