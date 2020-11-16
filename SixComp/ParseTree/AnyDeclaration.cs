using SixComp.Support;

namespace SixComp.ParseTree
{
    public interface AnyDeclaration : IWriteable
    {
        public static AnyDeclaration? TryParse(Parser parser)
        {
            switch (parser.Current.Kind)
            {
                case ToKind.KwLet:
                    return LetDeclaration.Parse(parser);
                case ToKind.KwVar:
                    return VarDeclaration.Parse(parser);
                case ToKind.KwFunc:
                    return FuncDeclaration.Parse(parser);
                case ToKind.KwClass:
                    return ClassDeclaration.Parse(parser);
                case ToKind.KwStruct:
                    return StructDeclaration.Parse(parser);
                case ToKind.KwEnum:
                    return EnumDeclaration.Parse(parser);
                case ToKind.KwCase:
                    return EnumCase.Parse(parser);
                case ToKind.KwInit:
                    return InitializerDeclaration.Parse(parser);
            }

            return null;
        }
    }
}
