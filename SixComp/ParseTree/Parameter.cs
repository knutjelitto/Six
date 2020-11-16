namespace SixComp.ParseTree
{
    public class Parameter
    {
        public Parameter(Name? label, Name name, AnyType type)
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

            if (parser.Next.Kind == ToKind.Name)
            {
                label = Name.Parse(parser);
            }

            var name = Name.Parse(parser);

            parser.Consume(ToKind.Colon);
            var type = AnyType.Parse(parser);

            return new Parameter(label, name, type);
        }

        public override string ToString()
        {
            var label = Label == null ? string.Empty : $"{Label} ";
            return $"{label}{Name}: {Type}";
        }
    }
}
