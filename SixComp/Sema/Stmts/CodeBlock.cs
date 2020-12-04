using SixComp.Support;

namespace SixComp.Sema
{
    public class CodeBlock : Base<Tree.CodeBlock>, IStatement
    {
        public CodeBlock(IScoped outer, Tree.CodeBlock tree)
            : base(outer, tree)
        {
            Statements = new Statements(this, tree.Statements);
        }

        public Statements Statements { get; }

        public static CodeBlock? Maybe(IScoped outer, Tree.CodeBlock? tree)
        {
            if (tree == null)
            {
                return null;
            }
            return new CodeBlock(outer, tree);
        }

        public override void Report(IWriter writer)
        {
            Statements.ReportList(writer, Strings.Head.Block);
        }
    }
}
