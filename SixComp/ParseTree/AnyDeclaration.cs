using SixComp.Support;
using System.Diagnostics;

namespace SixComp.ParseTree
{
    public interface AnyDeclaration : IWritable
    {
        public enum Context
        {
            Any,
            Top,
            Class,
            Enum,
            Extension,
            Protocol,
            Statement,
            Struct,
        }

        public static AnyDeclaration? TryParse(Parser parser, Context context)
        {
            var prefix = Prefix.Parse(parser, true);

            switch (parser.Current)
            {
                case ToKind.KwLet:
                    return LetDeclaration.Parse(parser, prefix);
                case ToKind.KwVar:
                    return AnyVarDeclaration.Parse(parser, prefix);
                case ToKind.KwFunc:
                    return FunctionDeclaration.Parse(parser, prefix);
                case ToKind.KwSubscript:
                    return SubscriptDeclaration.Parse(parser, prefix);
                case ToKind.KwClass:
                    return ClassDeclaration.Parse(parser, prefix);
                case ToKind.KwStruct:
                    return StructDeclaration.Parse(parser, prefix);
                case ToKind.KwEnum:
                    return EnumDeclaration.Parse(parser, prefix);
                case ToKind.KwCase when context == Context.Enum:
                    return EnumCase.Parse(parser);
                case ToKind.KwInit:
                    return InitializerDeclaration.Parse(parser, prefix);
                case ToKind.KwDeinit:
                    return DeinitializerDeclaration.Parse(parser, prefix);
                case ToKind.KwImport:
                    return ImportDeclaration.Parse(parser);
                case ToKind.KwExtension:
                    return ExtensionDeclaration.Parse(parser, prefix);
                case ToKind.KwTypealias:
                    return TypealiasDeclaration.Parse(parser, prefix);
                case ToKind.KwProtocol:
                    return ProtocolDeclaration.Parse(parser, prefix);
                case ToKind.KwPrecedencegroup:
                    return PrecGroupDeclaration.Parse(parser, prefix);
                case ToKind.KwAssociatedType:
                    return AssociatedTypeDeclaration.Parse(parser, prefix);
                case ToKind.KwPrefix when parser.Next == ToKind.KwOperator:
                case ToKind.KwPostfix when parser.Next == ToKind.KwOperator:
                case ToKind.KwInfix when parser.Next == ToKind.KwOperator:
                    return OperatorDeclaration.Parse(parser, prefix);

                case ToKind.CdIf:
                    return CcBlock.Parse(parser);
            }

            return null;
        }
    }
}
