using SixComp.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SixComp.Sema.Stmts
{
    public class PropertyBlocks : Items<PropertyBlock, Tree.PropertyBlocks>
    {
        public PropertyBlocks(IScoped outer, Tree.PropertyBlocks tree)
            : base(outer, tree, Enum(outer, tree))
        {
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.Blocks);
        }

        private static IEnumerable<PropertyBlock> Enum(IScoped outer, Tree.PropertyBlocks tree)
        {
            return tree.OrderBy(b => b.Value.index).Select(block => new PropertyBlock(outer, block.Value.block));
        }
    }
}
