using Six.Support;

namespace SixComp.Sema
{
    public class ClassDeclaration : Nominal<ParseTree.ClassDeclaration>
    {
        public ClassDeclaration(IScoped outer, ParseTree.ClassDeclaration tree)
            : base(outer, tree)
        {
            Declare(this);
        }

        public override void Report(IWriter writer)
        {
            Tree.Tree(writer);
            using (writer.Indent(Strings.Head.Class))
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
