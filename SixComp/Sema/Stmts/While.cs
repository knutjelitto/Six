using SixComp.Support;

namespace SixComp.Sema
{
    public class While : Base<Tree.WhileStatement>, IStatement
    {
        public While(IScoped outer, Tree.WhileStatement tree)
            : base(outer, tree)
        {
            Conditions = new Conditions(Outer, Tree.Conditions);
            Block = new CodeBlock(Outer, Tree.Block);
        }

        public Conditions Conditions { get; }
        public CodeBlock Block { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.While))
            {
                Conditions.Report(writer);
                Block.Report(writer);
            }
        }
    }
}
