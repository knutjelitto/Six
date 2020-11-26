namespace SixComp.ParseTree
{
    public class IdentifierPattern : SyntaxNode, AnyPattern
    {
        public IdentifierPattern(Name name)
        {
            Name = name;
        }

        public Name Name { get; }

        public static IdentifierPattern Parse(Parser parser)
        {
            var name = Name.Parse(parser);
            //var type = parser.Try(ToKind.Colon, TypeAnnotation.Parse);

            return new IdentifierPattern(name);
        }

        public bool NameOnly => true;

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
