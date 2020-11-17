using SixComp.Support;

namespace SixComp.ParseTree
{
    public interface AnyDeclaration : IWriteable
    {
        public static AnyDeclaration? TryParse(Parser parser)
        {
            var skip = true;
            while (skip)
            {
                switch (parser.Current.Kind)
                {
                    case ToKind.KwPublic:
                        parser.ConsumeAny();
                        break;
                    default:
                        skip = false;
                        break;
                }
            }
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
                case ToKind.KwImport:
                    return ImportDeclaration.Parse(parser);
                case ToKind.KwExtension:
                    return ExtensionDeclaration.Parse(parser);
            }

            return null;
        }
    }
}
