using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class FunctionCallExpression : Items<Trailing, Tree.FunctionCallExpression>, IExpression
    {
        public FunctionCallExpression(IScoped outer, Tree.FunctionCallExpression tree)
            : base(outer, tree, Enum(outer, tree))
        {
            Left = IExpression.Build(outer, Tree.Left);
            Arguments = new FunctionArguments(outer, tree.Arguments.Arguments);
        }

        public IExpression Left { get; }
        public FunctionArguments Arguments { get; }

        public override void Report(IWriter writer)
        {
            Tree.Tree(writer);
            using (writer.Indent(Strings.Head.Call))
            {
                Left.Report(writer, Strings.Head.Called);
                Arguments.Report(writer);
                this.ReportList(writer, Strings.Head.Trailings);
            }
        }

        private static IEnumerable<Trailing> Enum(IScoped outer, Tree.FunctionCallExpression tree)
        {
            return tree.Closures.Select(closure => new Trailing(outer, closure));
        }
    }
}
