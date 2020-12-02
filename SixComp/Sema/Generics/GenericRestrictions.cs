using SixComp.Support;
using System;

namespace SixComp.Sema
{
    public class GenericRestrictions : Items<GenericRestriction>
    {
        public GenericRestrictions(IScoped outer) : base(outer)
        {
        }

        public void Add(IScoped outer, Tree.GenericParameter tree)
        {
            if (tree.Requirement != null)
            {
                var left = new BaseName(outer, tree.Name);
                var right = IType.Build(outer, tree.Requirement.Composition);
                Add(new GenericConformance(outer, left, right));
            }
        }

        public void Add(IScoped outer, Tree.RequirementClause tree)
        {
            foreach (var req in tree.Requirements)
            {
                Add(Build(outer, req));
            }
        }

        private GenericRestriction Build(IScoped outer, Tree.AnyRequirement tree)
        {
            return Visit(outer, (dynamic)tree);
        }

        private GenericRestriction Visit(IScoped outer, Tree.SameTypeRequirement tree)
        {
            var left = IType.Build(outer, tree.Name);
            var right = IType.Build(outer, tree.Type);
            return new GenericSameType(outer, left, right);
        }

        private GenericRestriction Visit(IScoped outer, Tree.ConformanceRequirement tree)
        {
            var left = IType.Build(outer, tree.Name);
            var right = IType.Build(outer, tree.Composition);
            return new GenericConformance(outer, left, right);
        }

        private GenericRestriction Visit(IScoped outer, Tree.AnyRequirement tree)
        {
            throw new NotImplementedException();
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.Where);
        }
    }
}
