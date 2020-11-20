namespace SixComp.ParseTree
{
    public class TupleTypeElement : AnyType
    {
        public TupleTypeElement(Name? label, AnyType type)
        {
            Label = label;
            Type = type;
        }

        public Name? Label { get; }
        public AnyType Type { get; }

        public static TupleTypeElement Parse(Parser parser)
        {
            if (parser.Next == ToKind.Colon)
            {
                var label = Name.Parse(parser);
                var annotation = TypeAnnotation.Parse(parser);

                return new TupleTypeElement(label, annotation);
            }

            var type = AnyType.Parse(parser);
            return new TupleTypeElement(null, type);
        }

        public static TupleTypeElement From(Name? label, AnyType type)
        {
            return new TupleTypeElement(label, type);
        }

        public override string ToString()
        {
            var label = Label == null ? string.Empty : $"{Label} ";
            return $"{label}{Type}";
        }
    }
}
