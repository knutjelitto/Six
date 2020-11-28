namespace SixComp.ParseTree
{
    public class FunctionTypeArgument
    {
        public FunctionTypeArgument(Name? label, Name? name, AnyType type)
        {
            Label = label;
            Name = name;
            Type = type;
        }

        public Name? Label { get; }
        public Name? Name { get; }
        public AnyType Type { get; }

        public static FunctionTypeArgument Parse(Parser parser)
        {
            if (parser.Next == ToKind.Colon)
            {
                var name = Name.Parse(parser);
                var annotation = TypeAnnotation.Parse(parser);

                return new FunctionTypeArgument(null, name, annotation);
            }
            else if (parser.NextNext == ToKind.Colon)
            {
                var label = Name.Parse(parser);
                var name = Name.Parse(parser);
                var annotation = TypeAnnotation.Parse(parser);

                return new FunctionTypeArgument(label, name, annotation);
            }

            var type = PrefixedType.Parse(parser);
            return new FunctionTypeArgument(null, null, type);

        }

        public override string ToString()
        {
            var label = Label == null ? string.Empty : $"{Label} ";
            var name = Name == null ? string.Empty : $"{Name}";
            return $"{label}{name}{Type}";
        }
    }
}
