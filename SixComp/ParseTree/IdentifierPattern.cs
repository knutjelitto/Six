namespace SixComp.ParseTree
{
    public class IdentifierPattern : AnyPattern
    {
        public IdentifierPattern(Name name, AnyType? type)
        {
            Name = name;
            Type = type;
        }

        public Name Name { get; }
        public AnyType? Type { get; }

        public static IdentifierPattern Parse(Parser parser)
        {
            var name = Name.Parse(parser);
            var type = parser.TryMatch(ToKind.Colon, AnyType.Parse);

            return new IdentifierPattern(name, type);
        }

        public override string ToString()
        {
            var type = Type == null ? string.Empty : $": {Type}";
            return $"{Name}{type}";
        }
    }
}
