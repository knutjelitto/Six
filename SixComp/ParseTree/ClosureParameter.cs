namespace SixComp.ParseTree
{
    public class ClosureParameter
    {
        public ClosureParameter(Name name, TypeAnnotation? type)
        {
            Name = name;
            Type = type;
        }

        public Name Name { get; }
        public TypeAnnotation? Type { get; }

        public static ClosureParameter Parse(Parser parser)
        {
            var name = Name.Parse(parser);
            var type = parser.Try(ToKind.Colon, TypeAnnotation.Parse);

            return new ClosureParameter(name, type);
        }
    }
}
