using SixComp.Support;

namespace SixComp.Sema
{
    public class If : Base<Tree.IfStatement>, IStatement
    {
        public If(IScoped outer, Tree.IfStatement tree)
            : base(outer, tree)
        {
            Conditions = new Conditions(Outer, Tree.Conditions);
            Then = new CodeBlock(Outer, Tree.ThenPart);
            Else = IStatement.MaybeBuild(Outer, tree.ElsePart);
        }

        public Conditions Conditions { get; }
        public CodeBlock Then { get; }
        public IStatement? Else { get; }

        public override void Resolve(IWriter writer)
        {
            Resolve(writer, Conditions, Then, Else);
        }

        public override void Report(IWriter writer)
        {
            Tree.Tree(writer);
            using (writer.Indent(Strings.Head.If))
            {
                Conditions.Report(writer);
                Then.Report(writer, Strings.Head.Then);
                Else.Report(writer, Strings.Head.Enum);
            }
        }
    }
}
