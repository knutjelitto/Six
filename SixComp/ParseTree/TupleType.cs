namespace SixComp.ParseTree
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

        public static TupleType From(Prefix prefix, TupleTypeElementList elements)
        {
            return new TupleType(prefix, elements);
        }
    }
}
