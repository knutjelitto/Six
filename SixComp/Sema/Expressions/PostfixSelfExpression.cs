using SixComp.Support;

namespace SixComp.Sema
{
    public class PostfixSelf : Base<Tree.PostfixSelfExpression>, IExpression
    {
        public PostfixSelf(IScoped outer, Tree.PostfixSelfExpression tree)
            : base(outer, tree)
        {
            Left = IExpression.Build(Outer, Tree.Left);
            Operator = new BaseName(outer, Tree.Operator);
            Self = new BaseName(outer, Tree.Self);
        }
        public IExpression Left { get; }
        public BaseName Operator { get; }
        public BaseName Self { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Postfix))
            {
                Left.Report(writer, Strings.Head.Left);
                Operator.Report(writer, Strings.Head.Operator);
                Self.Report(writer, Strings.Head.Self);
            }
        }
    }
}
