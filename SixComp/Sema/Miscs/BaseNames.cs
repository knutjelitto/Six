using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class BaseNames : Items<BaseName>
    {
        public BaseNames(IScoped outer, IEnumerable<Tree.BaseName> names)
            : base(outer, names.Select(n => new BaseName(outer, n)))
        {
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.Names);
        }
    }
}
