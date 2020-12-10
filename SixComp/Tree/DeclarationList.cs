using System.Collections.Generic;

namespace SixComp
{
    public partial class ParseTree
    {
        public class DeclarationList : ItemList<IDeclaration>
        {
            public DeclarationList(List<IDeclaration> items) : base(items) { }
            public DeclarationList() { }

            public static DeclarationList Parse(Parser parser, IDeclaration.Context context)
            {
                var declarations = new List<IDeclaration>();

                while (IDeclaration.TryParse(parser, context) is IDeclaration declaration)
                {
                    declarations.Add(declaration);

                    while (parser.Match(ToKind.SemiColon))
                    {
                        ;
                    }
                }

                return new DeclarationList(declarations);
            }

            public override string ToString()
            {
                return string.Join(" ", this);
            }
        }
    }
}