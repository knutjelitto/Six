using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class FuncArguments : Items<FuncArgument, Tree.ArgumentList>
    {
        public FuncArguments(IScoped outer, Tree.ArgumentList tree)
            : base(outer, tree, Enum(outer, tree))
        {
        }

        private static IEnumerable<FuncArgument> Enum(IScoped outer, Tree.ArgumentList tree)
        {
            return tree.Select(argument => new FuncArgument(outer, argument));
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.Arguments);
        }
    }
}
