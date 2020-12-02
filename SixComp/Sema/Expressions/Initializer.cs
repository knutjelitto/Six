using SixComp.Support;
using SixComp.Tree;

namespace SixComp.Sema.Expressions
{
    public class Initializer : Base<Tree.InitializerExpression>, IExpression
    {
        public Initializer(IScoped outer, InitializerExpression tree) : base(outer, tree)
        {
            Left = IExpression.Build(outer, tree.Left);
            ArgumentNames = new BaseNames(outer, tree.Names);
        }

        public IExpression Left { get; }
        public BaseNames ArgumentNames { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.InitializerExpression))
            {
                Left.Report(writer);
                ArgumentNames.Report(writer);
            }
        }
    }
}
