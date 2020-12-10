using SixComp.Support;

namespace SixComp.Sema
{
    public class EnumDeclaration : Nominal<ParseTree.EnumDeclaration>
    {
        public EnumDeclaration(IScoped outer, ParseTree.EnumDeclaration tree)
            : base(outer, tree)
        {
            Declare(this);
        }

        public override void Report(IWriter writer)
        {
            Tree.Tree(writer);
            using (writer.Indent(Strings.Head.Enum))
            {
                Name.Report(writer, Strings.Head.Name);
                Generics.Report(writer);
                Inheritance.Report(writer);
                Where.Report(writer);
                Declarations.Report(writer);
            }
        }
    }
}
