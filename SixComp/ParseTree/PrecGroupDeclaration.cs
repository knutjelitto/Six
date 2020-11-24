namespace SixComp.ParseTree
{
    public class PrecGroupDeclaration : AnyDeclaration
    {
        public PrecGroupDeclaration(Prefix prefix, Name name, PrecGroupAttributeList attributes)
        {
            Prefix = prefix;
            Name = name;
            Attributes = attributes;
        }

        public Prefix Prefix { get; }
        public Name Name { get; }
        public PrecGroupAttributeList Attributes { get; }

        public static PrecGroupDeclaration Parse(Parser parser, Prefix prefix)
        {
            parser.Consume(ToKind.KwPrecedencegroup);
            var name = Name.Parse(parser);
            parser.Consume(ToKind.LBrace);
            var attributes = PrecGroupAttributeList.Parse(parser);
            parser.Consume(ToKind.RBrace);

            return new PrecGroupDeclaration(prefix, name, attributes);
        }
    }
}
