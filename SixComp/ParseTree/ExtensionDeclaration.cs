namespace SixComp.ParseTree
{
    public class ExtensionDeclaration : AnyDeclaration
    {
        public ExtensionDeclaration(TypeIdentifier name, TypeInheritanceClause inheritance, DeclarationList declarations)
        {
            Name = name;
            Inheritance = inheritance;
            Declarations = declarations;
        }

        public TypeIdentifier Name { get; }
        public TypeInheritanceClause Inheritance { get; }
        public DeclarationList Declarations { get; }

        public static ExtensionDeclaration Parse(Parser parser)
        {
            parser.Consume(ToKind.KwExtension);

            var name = TypeIdentifier.Parse(parser);
            var inheritance = parser.TryList(ToKind.Colon, TypeInheritanceClause.Parse);
            parser.Consume(ToKind.LBrace);
            var declarations = DeclarationList.Parse(parser);
            parser.Consume(ToKind.RBrace);

            return new ExtensionDeclaration(name, inheritance, declarations);
        }
    }
}
