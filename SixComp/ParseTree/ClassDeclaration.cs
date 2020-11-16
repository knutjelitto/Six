using SixComp.Support;

namespace SixComp.ParseTree
{
    public class ClassDeclaration : StructureType
    {
        public ClassDeclaration((Name name, GenericParameterList parameters, DeclarationList declarations, TypeInheritanceClause inheritance) args)
            : base(args)
        {
        }

        public static ClassDeclaration Parse(Parser parser)
        {
            return new ClassDeclaration(Parse(parser, ToKind.KwClass));
        }

        public override void Write(IWriter writer)
        {
            Write(writer, "class");
        }
    }
}
