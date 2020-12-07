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
            var candidates = Scope.LookUp(Name);
            Arguments.Resolve(writer);
            if (candidates.Count == 1)
            {
                Entity = candidates[0];
                return;
            }
            foreach (var candidate in candidates)
            {
                if (candidate.Generics?.Count == Arguments.Count)
                {
                    Entity = candidate;
                    return;
                }

            }
            UnResolve(writer, Name.Text);
        }

        public void ResolveChained(IWriter writer, IScoped scoped)
        {
            var candidates = scoped.Scope.Look(Name);
            Arguments.Resolve(writer);
            if (candidates.Count == 1)
            {
                Entity = candidates[0];
                return;
            }
            foreach (var candidate in candidates)
            {
                if (candidate.Generics?.Count == Arguments.Count)
                {
                    Entity = candidate;
                    return;
                }

            }
            UnResolve(writer, Name.Text);
        }

        public override void Report(IWriter writer)
        {
            Name.Report(writer, Strings.Head.Name);
            Arguments.Report(writer);
        }

        public override string ToString()
        {
            return $"{Name.Text}<{string.Join(",", Arguments)}>";
        }
    }
}
