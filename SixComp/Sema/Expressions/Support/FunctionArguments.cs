using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class FunctionArguments : Items<FunctionArgument, Tree.ArgumentList>
    {
        public FunctionArguments(IScoped outer, Tree.ArgumentList tree)
            : base(outer, tree, Enum(outer, tree))
        {
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.Arguments);
        }

        private static IEnumerable<FunctionArgument> Enum(IScoped outer, Tree.ArgumentList tree)
        {
            return tree.Select(argument => new FunctionArgument(outer, argument));
        }
    }
}
