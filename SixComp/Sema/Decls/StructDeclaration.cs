using SixComp.Support;

namespace SixComp.Sema
{
    public class StructDeclaration : Nominal<ParseTree.StructDeclaration>
    {
        public StructDeclaration(IScoped outer, ParseTree.StructDeclaration tree)
            : base(outer, tree)
        {
            Declare(this);
        }

        public override void Report(IWriter writer)
        {
            Tree.Tree(writer);
            using (writer.Indent(Strings.Head.Struct))
            {
                Name.Report(writer, Strings.Head.Name);
                Generics.Report(writer);
                Inheritance.Report(writer);
                Where.Report(writer);
                Declarations.Report(writer);
            }
        }

        public override string ToString()
        {
            return $"{Strings.KwStruct} {Name}";
        }
    }
}
