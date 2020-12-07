using SixComp.Support;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SixComp.Sema
{
    public class TypeIdentifier : Items<FullName, Tree.TypeIdentifier>, ITypeDefinition
    {
        public TypeIdentifier(IScoped outer, Tree.TypeIdentifier tree)
            : base(outer, tree, Enum(outer, tree))
        {
        }

        private bool IsSimplest => Count == 1 && this[0].IsSimplest;

        public override void Resolve(IWriter writer)
        {
            if (Count > 1)
            {
                Debug.Assert(true);
            }
            this.First().Resolve(writer);
            var entity = this.First().Entity;
            if (entity != null)
            {
                foreach (var fullName in this.Skip(1))
                {
                    fullName.ResolveChained(writer, entity);
                }
            }
            // TODO: TODO
            //UnResolve(writer);
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.Path);
        }

        public override string ToString()
        {
            if (IsSimplest)
            {
                return this[0].Name.Text;
            }
            return base.ToString()!;
        }

        private static IEnumerable<FullName> Enum(IScoped outer, Tree.TypeIdentifier identifier)
        {
            return identifier.Select(name => new FullName(outer, name));
        }
    }
}
