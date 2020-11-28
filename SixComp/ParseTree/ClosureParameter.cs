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

        public bool NameOnly => Type == null;

        public static ClosureParameter? TryParse(Parser parser, bool nameOnly)
        {
            var name = Name.TryParse(parser, withImplicits: false);
            if (name == null)
            {
                return null;
            }

            var type = nameOnly
                ? null
                : parser.Try(ToKind.Colon, TypeAnnotation.Parse);

            return new ClosureParameter(name, type);
        }

        public override string ToString()
        {
            return $"{Name}{Type}";
        }
    }
}
