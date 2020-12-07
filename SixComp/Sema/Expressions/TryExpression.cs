using SixComp.Common;
using SixComp.Support;

namespace SixComp.Sema
{
    public class TryExpression : Base<Tree.TryExpression>, IExpression
    {
        public TryExpression(IScoped outer, Tree.TryExpression tree) : base(outer, tree)
        {
            Kind = tree.Try.Kind;
            Expression = IExpression.Build(Outer, Tree.Expression);
        }

        public TryKind Kind { get; }
        public IExpression Expression { get; }

        public override void Resolve(IWriter writer)
        {
            Resolve(writer, Expression);
        }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Try))
            {
                Kind.Report(writer, Strings.Head.Kind);
                Expression.Report(writer);
            }
        }
    }
}
