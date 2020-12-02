using SixComp.Support;
using System;

namespace SixComp.Sema.Stmts
{
    public class PropertyBlock : Base<Tree.PropertyBlock>
    {
        public PropertyBlock(IScoped outer, Tree.PropertyBlock tree)
            : base(outer, tree)
        {
            Kind = tree.Kind switch
            {
                SixComp.Tree.PropertyBlock.BlockKind.Get => BlockKind.Get,
                SixComp.Tree.PropertyBlock.BlockKind.GetDefault => BlockKind.GetDefault,
                SixComp.Tree.PropertyBlock.BlockKind.Set => BlockKind.Set,
                SixComp.Tree.PropertyBlock.BlockKind.WillSet => BlockKind.WillSet,
                SixComp.Tree.PropertyBlock.BlockKind.DidSet => BlockKind.DidSet,
                SixComp.Tree.PropertyBlock.BlockKind.Special => BlockKind.Special,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        public BlockKind Kind { get; }

        public override void Report(IWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
