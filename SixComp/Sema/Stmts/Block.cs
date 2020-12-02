using SixComp.Support;

namespace SixComp.Sema
{
    public class Block : Statement<Tree.CodeBlock>
    {
        public Block(IScoped outer, Tree.CodeBlock tree)
            : base(outer, tree)
        {
            Statements = new Statements(this, tree.Statements);
        }

        public Statements Statements { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Block))
            {
                Statements.Report(writer);
            }
        }
    }
}
