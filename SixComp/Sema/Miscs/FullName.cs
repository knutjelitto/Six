using SixComp.Entities;
using SixComp.Support;
using System.Collections.Generic;

namespace SixComp.Sema
{
    public class FullName : Base<Tree.FullName>, ITypeDefinition, IExpression, INamed
    {
        public FullName(IScoped outer, Tree.FullName tree)
            : base(outer, tree)
        {
            Name = new BaseName(Outer, Tree.Name);
            Arguments = new GenericArguments(Outer, Tree.Arguments);
        }

        public BaseName Name { get; }
        public GenericArguments Arguments { get; }

        public bool IsSimplest => Arguments.Count == 0;

        public override void Resolve(IWriter writer)
        {
            var canditates = Scope.LookUp(Name);
            Arguments.Resolve(writer);
            if (canditates.Count == 1)
            {
                Entity = canditates[0];
                return;
            }
            if (canditates.Count == 0)
            {
                UnResolve(writer);
            }
        }

        public IReadOnlyList<IEntity> ResolveChained(IWriter writer, IScoped scoped)
        {
            var candidates = scoped.Scope.Look(Name);
            Arguments.Resolve(writer);
            return candidates;
        }

        public override void Report(IWriter writer)
        {
            Name.Report(writer, Strings.Head.Name);
            Arguments.Report(writer);
        }

        public override string ToString()
        {
            if (IsSimplest)
            {
                return Name.Text;
            }
            return base.ToString()!;
        }
    }
}
