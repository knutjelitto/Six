namespace SixComp
{
    public partial class ParseTree
    {
        public class TupleTypeElement : IType
        {
            public TupleTypeElement(BaseName? label, IType type)
            {
                Label = label;
                Type = type;
            }

            public BaseName? Label { get; }
            public IType Type { get; }

            public static TupleTypeElement Parse(Parser parser)
            {
                if (parser.Next == ToKind.Colon)
                {
                    var label = BaseName.Parse(parser);
                    var annotation = TypeAnnotation.Parse(parser);

                    return new TupleTypeElement(label, annotation);
                }

                var type = IType.Parse(parser);
                return new TupleTypeElement(null, type);
            }

            public static TupleTypeElement From(BaseName? label, IType type)
            {
                return new TupleTypeElement(label, type);
            }

            public override string ToString()
            {
                return $"{Label}{Type}";
            }
        }
    }
}