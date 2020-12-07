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


        public override void Resolve(IWriter writer)
        {
            if (Count > 1)
            {
                Debug.Assert(true);
            }
            var anchor = this.First();
            anchor.Resolve(writer);
            var entity = anchor.Entity;
            if (entity != null)
            {
                foreach (var fullName in this.Skip(1))
                {
                    fullName.ResolveChained(writer, entity);
                    entity = fullName.Entity;
                    if (entity == null)
                    {
                        break;
                    }
                }
            }
            Entity = entity;
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.Path);
        }

        public override string ToString()
        {
            return string.Join(".", this);
        }

        private static IEnumerable<FullName> Enum(IScoped outer, Tree.TypeIdentifier identifier)
        {
            return identifier.Select(name => new FullName(outer, name));
        }
    }
}
