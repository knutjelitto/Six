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
                    return AnyVarDeclaration.Parse(parser, prefix);
                case ToKind.KwFunc:
                    return FunctionDeclaration.Parse(parser);
                case ToKind.KwClass:
                    return ClassDeclaration.Parse(parser, prefix);
                case ToKind.KwStruct:
                    return StructDeclaration.Parse(parser, prefix);
                case ToKind.KwEnum:
                    return EnumDeclaration.Parse(parser, prefix);
                case ToKind.KwCase:
                    return EnumCase.Parse(parser);
                case ToKind.KwInit:
                    return InitializerDeclaration.Parse(parser);
                case ToKind.KwImport:
                    return ImportDeclaration.Parse(parser);
                case ToKind.KwExtension:
                    return ExtensionDeclaration.Parse(parser, prefix);
                case ToKind.KwTypealias:
                    return TypealiasDeclaration.Parse(parser, prefix);
                case ToKind.KwProtocol:
                    return ProtocolDeclaration.Parse(parser, prefix);

                case ToKind.CdIf:
                    return CcBlock.Parse(parser);
            }

            return null;
        }
    }
}
