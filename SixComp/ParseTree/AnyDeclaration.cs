using SixComp.Support;

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
            var offset = parser.Offset;

            var prefix = Prefix.PreParse(parser);

            switch (prefix.Last)
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
                    return EnumCase.Parse(parser, prefix);
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
                case ToKind.KwAssociatedType:
                    return AssociatedTypeDeclaration.Parse(parser, prefix);
                case ToKind.KwPrefix when parser.Next == ToKind.KwOperator:
                case ToKind.KwPostfix when parser.Next == ToKind.KwOperator:
                case ToKind.KwInfix when parser.Next == ToKind.KwOperator:
                    return OperatorDeclaration.Parse(parser, prefix);

                case ToKind.CdIf:
                    CcBlock.Ignore(parser, force: true);
                    return AnyDeclaration.TryParse(parser, context);

                default:
                    if (context == Context.Enum && parser.Match(ToKind.KwCase))
                    {
                        return EnumCase.Parse(parser, prefix);
                    }
                    if (Prefix.Fixes.Contains(parser.Current) && parser.Next == ToKind.KwOperator)
                    {
                        return OperatorDeclaration.Parse(parser, prefix);
                    }
                    switch (parser.CurrentToken.Text)
                    {
                        case Contextual.Precedencegroup:
                            return PrecGroupDeclaration.Parse(parser, prefix);
                    }
                    break;
            }

            parser.Offset = offset;
            return null;
        }
    }
}
