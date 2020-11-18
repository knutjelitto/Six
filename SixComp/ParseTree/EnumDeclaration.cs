using SixComp.Support;

namespace SixComp.ParseTree
{
    public class EnumDeclaration : StructureType
    {
        public EnumDeclaration((Name name, GenericParameterClause parameters, DeclarationList declarations, TypeInheritanceClause inheritance) args)
            : base(args)
        {
        }

        public static EnumDeclaration Parse(Parser parser)
        {
            return new EnumDeclaration(Parse(parser, ToKind.KwEnum));
        }

        public override void Write(IWriter writer)
        {
            Write(writer, "enum");
        }
    }
}
