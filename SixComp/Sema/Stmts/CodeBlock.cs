using SixComp.Support;

namespace SixComp.Sema
{
    public class CodeBlock : BaseScoped<ParseTree.CodeBlock>, IStatement
    {
        public CodeBlock(IScoped outer, ParseTree.CodeBlock tree)
            : base(outer, tree)
        {
            Statements = new Statements(this, tree.Statements);
        }

        public Statements Statements { get; }

        public override void Report(IWriter writer)
        {
            Statements.ReportList(writer, Strings.Head.Block);
        }

        public static CodeBlock? Maybe(IScoped outer, ParseTree.CodeBlock? tree)
        {
            if (tree == null)
            {
                return null;
            }
            return new CodeBlock(outer, tree);
        }
    }
}
