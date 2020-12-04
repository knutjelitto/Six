using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class BlockVar : Items<PropertyBlock, Tree.VarDeclaration>, IDeclaration, INamed
    {
        public BlockVar(IScoped outer, Tree.VarDeclaration tree)
            : base(outer, tree, Enum(outer, tree))
        {
            Name = new BaseName(outer, tree.Name);
            Type = IType.MaybeBuild(outer, tree.Type);
        }

        public BaseName Name { get; }
        public IType? Type { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Var))
            {
                Name.Report(writer, Strings.Head.Name);
                Type.Report(writer, Strings.Head.Type);
                this.ReportList(writer, Strings.Head.Blocks);
            }
        }

        private static IEnumerable<PropertyBlock> Enum(IScoped outer, Tree.VarDeclaration tree)
        {
            return tree.Blocks.OrderBy(b => b.Value.index).Select(block => new PropertyBlock(outer, block.Value.block));
        }
    }
}
