namespace SixComp.Tree
{
    public class TupleTypeElement : AnyType
    {
        public TupleTypeElement(BaseName? label, AnyType type)
        {
            Label = label;
            Type = type;
        }

        public BaseName? Label { get; }
        public AnyType Type { get; }

        public static TupleTypeElement Parse(Parser parser)
        {
            if (parser.Next == ToKind.Colon)
            {
                var label = BaseName.Parse(parser);
                var annotation = TypeAnnotation.Parse(parser);

                return new TupleTypeElement(label, annotation);
            }

            var type = AnyType.Parse(parser);
            return new TupleTypeElement(null, type);
        }

        public static TupleTypeElement From(BaseName? label, AnyType type)
        {
            return new TupleTypeElement(label, type);
        }

        public override string ToString()
        {
            return $"{Label}{Type}";
        }
    }
}
