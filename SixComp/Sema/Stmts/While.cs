using SixComp.Support;

namespace SixComp.Sema
{
    public class While : Base<Tree.WhileStatement>, IStatement
    {
        public While(IScoped outer, Tree.WhileStatement tree)
            : base(outer, tree)
        {
            Conditions = new Conditions(Outer, Tree.Conditions);
            Block = new Block(Outer, Tree.Block);
        }

        public Conditions Conditions { get; }
        public Block Block { get; }

        public override void Report(IWriter writer)
        {
            writer.WriteLine($"{Strings.While}");
            using (writer.Indent())
            {
                Conditions.Report(writer);
                Block.Report(writer);
            }
        }
    }
}
