using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class TypeIdentifier : Items<FullName>, IType
    {
        public TypeIdentifier(IScoped outer, Tree.TypeIdentifier tree)
            : base(outer, EnumNames(outer, tree))
        {
            Tree = tree;
        }

        public Tree.TypeIdentifier Tree { get; }

        private bool IsSimplest => Count == 1 && this[0].IsSimplest;

        private static IEnumerable<FullName> EnumNames(IScoped outer, Tree.TypeIdentifier identifier)
        {
            return identifier.Select(name => new FullName(outer, name));
        }

        public override string ToString()
        {
            if (IsSimplest)
            {
                return this[0].Name.Text;
            }
            return base.ToString()!;
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.Path);
        }
    }
}
