namespace SixComp.ParseTree
{
    public class TupleTypeItem
    {
        public TupleTypeItem(Name? name, AnyType type)
        {
            Name = name;
            Type = type;
        }

        public Name? Name { get; }
        public AnyType Type { get; }

        public static TupleTypeItem Parse(Parser parser)
        {
            Name? name = null;

            if (parser.Next.Kind == ToKind.Colon)
            {
                name = Name.Parse(parser);
                parser.Consume(ToKind.Colon);
            }

            var type = AnyType.Parse(parser);

            return new TupleTypeItem(name, type);
        }

        public override string ToString()
        {
            var name = Name == null ? string.Empty : $": {Name}";
            return $"{name}{Type}";
        }
    }
}
