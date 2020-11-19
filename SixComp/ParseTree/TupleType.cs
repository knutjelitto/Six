namespace SixComp.ParseTree
{
    public class TupleType : AnyType
    {
        private TupleType(Prefix prefix, LabeledTypeList items)
        {
            Prefix = prefix;
            Items = items;
        }

        public Prefix Prefix { get; }
        public LabeledTypeList Items { get; }

        public static TupleType From(Prefix prefix, LabeledTypeList items)
        {
            return new TupleType(prefix, items);
        }
    }
}
