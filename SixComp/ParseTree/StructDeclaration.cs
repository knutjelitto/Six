using SixComp.Support;

namespace SixComp.ParseTree
{
    public class StructDeclaration : StructureType
    {
        public StructDeclaration((Name name, GenericParameterList parameters, DeclarationList declarations, TypeInheritanceClause inheritance) args)
            : base(args)
        {
        }

        public static StructDeclaration Parse(Parser parser)
        {
            return new StructDeclaration(Parse(parser, ToKind.KwStruct));
        }

        public override void Write(IWriter writer)
        {
            Write(writer, "struct");
        }
    }
}
