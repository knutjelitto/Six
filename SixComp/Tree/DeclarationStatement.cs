using SixComp.Support;

namespace SixComp
{
    public partial class ParseTree
    {
        public class DeclarationStatement : IStatement, IDeclaration
        {
            public DeclarationStatement(IDeclaration declaration)
            {
                Declaration = declaration;
            }

            public IDeclaration Declaration { get; }

            public static DeclarationStatement? TryParse(Parser parser)
            {
                var declaration = IDeclaration.TryParse(parser, IDeclaration.Context.Statement);

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
}