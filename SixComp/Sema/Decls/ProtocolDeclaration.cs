using SixComp.Support;

namespace SixComp.Sema
{
    public class ProtocolDeclaration : Nominal<Tree.ProtocolDeclaration>
    {
        public ProtocolDeclaration(IScoped outer, Tree.ProtocolDeclaration tree)
            : base(outer, tree)
        {
            Declare(this);
        }

        public override void Report(IWriter writer)
        {
            Tree.Tree(writer);
            using (writer.Indent($"{Strings.Head.Protocol} {Name.Text}"))
            {
                Generics.Report(writer);
                Inheritance.Report(writer);
                Where.Report(writer);
                Declarations.Report(writer);
            }
        }
    }
}
