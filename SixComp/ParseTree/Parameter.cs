namespace SixComp.ParseTree
{
    public class Parameter
    {
        public Parameter(Name? label, Name name, AnyType type, Initializer? initializer)
        {
            Label = label;
            Name = name;
            Type = type;
            Initializer = initializer;
        }

        public Name? Label { get; }
        public Name Name { get; }
        public AnyType Type { get; }
        public Initializer? Initializer { get; }

        public static Parameter Parse(Parser parser)
        {
            var attributes = AttributeList.TryParse(parser);
            Name? label = null;
            if (parser.NextNext == ToKind.Colon)
            {
                label = Name.Parse(parser);
            }
            var name = Name.Parse(parser);
            var type = TypeAnnotation.Parse(parser);
            var initializer = parser.Try(ToKind.Assign, Initializer.Parse);

            return new Parameter(label, name, type, initializer);
        }

        public override string ToString()
        {
            var label = Label == null ? string.Empty : $"{Label} ";
            return $"{label}{Name}: {Type}";
        }
    }
}
