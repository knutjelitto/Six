using SixComp.Support;

namespace SixComp.ParseTree
{
    public interface AnyDeclaration : IWritable
    {
        public static AnyDeclaration? TryParse(Parser parser)
        {
            var prefix = Prefix.Parse(parser);

            switch (parser.Current)
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

                case ToKind.CdIf:
                    return CcBlock.Parse(parser);
            }

            return null;
        }
    }
}
