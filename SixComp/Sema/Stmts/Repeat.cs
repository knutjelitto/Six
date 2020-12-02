using SixComp.Support;

namespace SixComp.Sema
{
    public class Repeat : Base<Tree.RepeatStatement>, IStatement
    {
        public Repeat(IScoped outer, Tree.RepeatStatement tree)
            : base(outer, tree)
        {
            Block = new Block(Outer, Tree.Block);
            Condition = IExpression.Build(Outer, Tree.Condition);
        }

        public Block Block { get; }
        public IExpression Condition { get; }

        public override void Report(IWriter writer)
        {
            writer.WriteLine(Strings.Repeat);
            using (writer.Indent())
            {
                Block.Report(writer);
                Condition.Report(writer);
            }
        }
    }
}
