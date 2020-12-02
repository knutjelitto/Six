using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class Tuple : Items<NamedValue, Tree.TupleExpression>, IExpression
    {
        public Tuple(IScoped outer, Tree.TupleExpression tree)
            : base(outer, tree, Enum(outer, tree))
        {
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.TupleExpression);
        }
        private static IEnumerable<NamedValue> Enum(IScoped outer, Tree.TupleExpression tree)
        {
            return tree.Elements.Select(element => new NamedValue(outer, element));
        }
    }
}
