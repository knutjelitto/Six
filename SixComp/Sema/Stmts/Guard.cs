﻿using Six.Support;

namespace SixComp.Sema
{
    public class Guard : Base<ParseTree.GuardStatement>, IStatement
    {
        public Guard(IScoped outer, ParseTree.GuardStatement tree)
            : base(outer, tree)
        {
            Conditions = new Conditions(Outer, Tree.Conditions);
            Block = new CodeBlock(Outer, Tree.Block);
        }

        public Conditions Conditions { get; }
        public CodeBlock Block { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Guard))
            {
                Conditions.Report(writer);
                Block.Report(writer);
            }
        }
    }
}
