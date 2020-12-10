using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class GenericArguments : Items<GenericArgument>
    {
        public GenericArguments(IScoped outer, ParseTree.GenericArgumentClause tree)
            : base(outer, EnumArguments(outer, tree.Arguments))
        {
            Tree = tree;
        }

        public ParseTree.GenericArgumentClause Tree { get; }

        public override void Report(IWriter writer)
        {
            if (Count > 0)
            {
                this.ReportList(writer, Strings.Head.GenericArguments);
            }
        }

        private static IEnumerable<GenericArgument> EnumArguments(IScoped outer, IEnumerable<ParseTree.GenericArgument> arguments)
        {
            return arguments.Select(argument => new GenericArgument(outer, argument));
        }
    }
}
