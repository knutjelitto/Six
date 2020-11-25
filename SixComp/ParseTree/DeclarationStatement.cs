using SixComp.Support;

namespace SixComp.ParseTree
{
    public class DeclarationStatement : AnyStatement
    {
        public DeclarationStatement(AnyDeclaration declaration)
        {
            Declaration = declaration;
        }

        public AnyDeclaration Declaration { get; }

        public static DeclarationStatement? TryParse(Parser parser)
        {
            var declaration = AnyDeclaration.TryParse(parser, AnyDeclaration.Context.Statement);

            if (declaration != null)
            {
                return new DeclarationStatement(declaration);
            }

            return null;
        }

        public void Write(IWriter writer)
        {
            Declaration.Write(writer);
        }

        public override string ToString()
        {
            return $"{Declaration}";
        }
    }
}
