using System.Collections.Generic;

namespace SixComp.ParseTree
{
    public class DeclarationList : ItemList<AnyDeclaration>
    {
        public DeclarationList(List<AnyDeclaration> items) : base(items) { }
        public DeclarationList() { }

        public static DeclarationList Parse(Parser parser, AnyDeclaration.Context context)
        {
            var declarations = new List<AnyDeclaration>();

            while (AnyDeclaration.TryParse(parser, context) is AnyDeclaration declaration)
            {
                declarations.Add(declaration);

                while (parser.Match(ToKind.SemiColon))
                {
                    ;
                }
            }

            return new DeclarationList(declarations);
        }
    }
}
