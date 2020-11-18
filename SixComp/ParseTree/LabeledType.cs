namespace SixComp.ParseTree
{
    public class LabeledType : AnyType
    {
        public LabeledType(Name label, TypeAnnotation type)
        {
            Label = label;
            Type = type;
        }

        public Name Label { get; }
        public TypeAnnotation Type { get; }

        public static LabeledType Parse(Parser parser)
        {
            var label = Name.Parse(parser);
            var type = TypeAnnotation.Parse(parser);

            return new LabeledType(label, type);
        }

        public override string ToString()
        {
            return $"{Label}{Type}";
        }
    }
}
