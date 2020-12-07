using SixComp.Support;

namespace SixComp.Sema
{
    public class CodeBlock : BaseScoped<Tree.CodeBlock>, IStatement
    {
        public CodeBlock(IScoped outer, Tree.CodeBlock tree)
            : base(outer, tree)
        {
            Statements = new Statements(this, tree.Statements);
        }

        public Statements Statements { get; }

        public override void Resolve(IWriter writer)
        {
            Statements.Resolve(writer);
        }

        public override void Report(IWriter writer)
        {
            Statements.ReportList(writer, Strings.Head.Block);
        }

        public static CodeBlock? Maybe(IScoped outer, Tree.CodeBlock? tree)
        {
            if (tree == null)
            {
                return null;
            }
            return new CodeBlock(outer, tree);
        }
    }
}
