using SixComp.Support;

namespace SixComp.Sema
{
    public class Repeat : Base<ParseTree.RepeatStatement>, IStatement
    {
        public Repeat(IScoped outer, ParseTree.RepeatStatement tree)
            : base(outer, tree)
        {
            Block = new CodeBlock(Outer, Tree.Block);
            Condition = IExpression.Build(Outer, Tree.Condition);
        }

        public CodeBlock Block { get; }
        public IExpression Condition { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Repeat))
            {
                Block.Report(writer);
                Condition.Report(writer);
            }
        }
    }
}
