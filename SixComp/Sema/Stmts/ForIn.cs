using Six.Support;

namespace SixComp.Sema
{
    public class ForIn : Base<ParseTree.ForInStatement>, IStatement
    {
        public ForIn(IScoped outer, ParseTree.ForInStatement tree)
            : base(outer, tree)
        {
            Pattern = IPattern.Build(Outer, tree.Pattern);
            Values = IExpression.Build(Outer, tree.Expression);
            Where = IExpression.MaybeBuild(Outer, tree.Where?.Expression);
            Block = new CodeBlock(Outer, tree.Block);
        }

        public IPattern Pattern { get; }
        public IExpression Values { get; }
        public IExpression? Where { get; }
        public CodeBlock Block { get; }

        public override void Report(IWriter writer)
        {
            Tree.Tree(writer);
            using (writer.Indent(Strings.Head.ForIn))
            {
                Pattern.Report(writer, Strings.Head.Pattern);
                Values.Report(writer, Strings.Head.Values);
                Where?.Report(writer, Strings.Head.Where);
                Block.Report(writer);
            }
        }
    }
}
