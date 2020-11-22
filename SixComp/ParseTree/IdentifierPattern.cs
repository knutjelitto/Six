namespace SixComp.ParseTree
{
    public class IdentifierPattern : SyntaxNode, AnyPattern
    {
        public IdentifierPattern(Name name, TypeAnnotation? type)
        {
            Name = name;
            Type = type;
        }

        public Name Name { get; }
        public TypeAnnotation? Type { get; }

        public static IdentifierPattern Parse(Parser parser)
        {
            var name = Name.Parse(parser);
            var type = parser.Try(ToKind.Colon, TypeAnnotation.Parse);

            return new IdentifierPattern(name, type);
        }

        public bool NameOnly => true;

        public override string ToString()
        {
            var type = Type == null ? string.Empty : $": {Type}";
            return $"{Name}{type}";
        }
    }
}
