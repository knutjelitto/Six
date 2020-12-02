using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class GenericArguments : Items<GenericArgument>
    {
        public GenericArguments(IScoped outer, Tree.GenericArgumentClause tree)
            : base(outer, EnumArguments(outer, tree.Arguments))
        {
            Tree = tree;
        }

        public Tree.GenericArgumentClause Tree { get; }


        public override void Report(IWriter writer)
        {
            if (Count > 0)
            {
                this.ReportList(writer, Strings.Head.GenericArguments);
            }
        }

        private static IEnumerable<GenericArgument> EnumArguments(IScoped outer, IEnumerable<Tree.GenericArgument> arguments)
        {
            return arguments.Select(argument => new GenericArgument(outer, argument));
        }
    }
}
