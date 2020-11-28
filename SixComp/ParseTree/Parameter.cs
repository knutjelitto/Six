namespace SixComp.ParseTree
{
    public class Parameter
    {
        public Parameter(Prefix prefix, Name? label, Name name, TypeAnnotation type, bool variadic, Initializer? initializer)
        {
            Label = label;
            Name = name;
            Type = type;
            Variadic = variadic;
            Initializer = initializer;
        }

        public Name? Label { get; }
        public Name Name { get; }
        public TypeAnnotation Type { get; }
        public bool Variadic { get; }
        public Initializer? Initializer { get; }

        public static Parameter Parse(Parser parser)
        {
            var prefix = Prefix.PreParse(parser, onlyAttributes: true);
            Name? label = null;
            if (parser.NextNext == ToKind.Colon)
            {
                label = Name.Parse(parser);
            }
            var name = Name.Parse(parser);
            var type = TypeAnnotation.Parse(parser);
            var variadic = parser.Match(ToKind.DotDotDot);
            var initializer = parser.Try(ToKind.Assign, Initializer.Parse);

            return new Parameter(prefix, label, name, type, variadic, initializer);
        }

        public override string ToString()
        {
            var space = Label == null ? string.Empty : " ";
            var variadic = Variadic ? "..." : string.Empty;
            return $"{Label}{space}{Name}{Type}{variadic}{Initializer}";
        }
    }
}
