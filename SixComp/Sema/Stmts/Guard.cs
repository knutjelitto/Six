using SixComp.Support;

namespace SixComp.Sema
{
    public class Guard : Base<Tree.GuardStatement>, IStatement
    {
        public Guard(IScoped outer, Tree.GuardStatement tree)
            : base(outer, tree)
        {
            Conditions = new Conditions(Outer, Tree.Conditions);
            Block = new Block(Outer, Tree.Block);
        }

        public Conditions Conditions { get; }
        public Block Block { get; }

        public override void Report(IWriter writer)
        {
            writer.WriteLine(Strings.Guard);
            using (writer.Indent())
            {
                Conditions.Report(writer);
                Block.Report(writer);
            }
        }
    }
}
