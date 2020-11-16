using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class DeclarationList : ItemList<AnyDeclaration>
    {
        public DeclarationList(List<AnyDeclaration> items) : base(items) { }
        public DeclarationList() { }

        public static DeclarationList Parse(Parser parser)
        {
            var declarations = new List<AnyDeclaration>();

            while (AnyDeclaration.TryParse(parser) is AnyDeclaration declaration)
            {
                declarations.Add(declaration);
            }

            return new DeclarationList(declarations);
        }
    }
}
