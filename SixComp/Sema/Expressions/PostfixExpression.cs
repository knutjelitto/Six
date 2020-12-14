using Six.Support;

namespace SixComp.Sema
{
    public class PostfixExpression : Base<ParseTree.PostfixOpExpression>, IExpression
    {
        public PostfixExpression(IScoped outer, ParseTree.PostfixOpExpression tree)
            : base(outer, tree)
        {
            Left = IExpression.Build(Outer, Tree.Left);
            Operator = new BaseName(Outer, Tree.Operator);
        }
        
        public IExpression Left { get; }
        public BaseName Operator { get; }

        public override void Report(IWriter writer)
        {
            using (writer.Indent(Strings.Head.Postfix))
            {
                Left.Report(writer, Strings.Head.Left);
                Operator.Report(writer, Strings.Head.Operator);
            }
        }
    }
}
