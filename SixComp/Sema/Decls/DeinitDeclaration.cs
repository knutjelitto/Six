﻿using SixComp.Support;

namespace SixComp.Sema
{
    public class DeinitDeclaration : Base<Tree.DeinitializerDeclaration>, IDeclaration
    {
        public DeinitDeclaration(IScoped outer, Tree.DeinitializerDeclaration tree)
            : base(outer, tree)
        {
            Block = new CodeBlock(outer, tree.Block);
        }

        public CodeBlock Block { get; }

        public override void Report(IWriter writer)
        {
            Tree.Tree(writer);
            using (writer.Indent(Strings.Head.Deinit))
            {
                Block.Report(writer);
            }
        }
    }
}
