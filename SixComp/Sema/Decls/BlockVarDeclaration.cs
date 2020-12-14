using Six.Support;
using SixComp.Sema.Stmts;

namespace SixComp.Sema
{
    public class BlockVarDeclaration : BaseScoped<ParseTree.VarDeclaration>, INamedDeclaration
    {
        public BlockVarDeclaration(IScoped outer, ParseTree.VarDeclaration tree)
            : base(outer, tree)
        {
            Name = new BaseName(outer, tree.Name);
            Type = ITypeDefinition.MaybeBuild(outer, tree.Type);
            Blocks = new PropertyBlocks(Outer, tree.Blocks);

            Declare(this);
        }

        public BaseName Name { get; }
        public ITypeDefinition? Type { get; }
        public PropertyBlocks Blocks { get; }

        public override void Report(IWriter writer)
        {
            Tree.Tree(writer);
            using (writer.Indent(Strings.Head.Var))
            {
                Name.Report(writer, Strings.Head.Name);
                Type.Report(writer, Strings.Head.Type);
                Blocks.Report(writer);
            }
        }
    }
}
