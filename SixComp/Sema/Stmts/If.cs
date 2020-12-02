using SixComp.Support;

namespace SixComp.Sema
{
    public class If : Base<Tree.IfStatement>, IStatement
    {
        public If(IScoped outer, Tree.IfStatement tree)
            : base(outer, tree)
        {
            Conditions = new Conditions(Outer, Tree.Conditions);
            Then = new Block(Outer, Tree.ThenPart);
            Else = IStatement.MaybeBuild(Outer, tree.ElsePart);
        }

        public Conditions Conditions { get; }
        public Block Then { get; }
        public IStatement? Else { get; }

        public override void Report(IWriter writer)
        {
            writer.WriteLine(Strings.If);
            using (writer.Indent())
            {
                Conditions.Report(writer);
                writer.WriteLine(Strings.Then);
                writer.Indent(() => Then.Report(writer));
                if (Else != null)
                {
                    writer.WriteLine(Strings.Else);
                    writer.Indent(() => Else.Report(writer));
                }
            }
        }
    }
}
