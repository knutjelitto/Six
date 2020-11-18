namespace SixComp.ParseTree
{
    public class Parameter
    {
        public Parameter(Name? label, Name name, TypeAnnotation type)
        {
            Label = label;
            Name = name;
            Type = type;
        }

        public Name? Label { get; }
        public Name Name { get; }
        public AnyType Type { get; }

        public static Parameter Parse(Parser parser)
        {
            Name? label = null;

            if (parser.Next == ToKind.Name)
            {
                label = Name.Parse(parser);
            }

            var name = Name.Parse(parser);

            var type = TypeAnnotation.Parse(parser);

            return new Parameter(label, name, type);
        }

        public override string ToString()
        {
            var label = Label == null ? string.Empty : $"{Label} ";
            return $"{label}{Name}: {Type}";
        }
    }
}
