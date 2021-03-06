﻿using Six.Support;
using System;

namespace SixComp.Sema
{
    public class GenericRestrictions : Items<GenericRestriction>
    {
        public GenericRestrictions(IScoped outer) : base(outer)
        {
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.Where);
        }

        public void Add(IScoped outer, ParseTree.GenericParameter tree)
        {
            if (tree.Requirement != null)
            {
                var left = new BaseName(outer, tree.Name);
                var right = ITypeDefinition.Build(outer, tree.Requirement.Composition);
                Add(new GenericConformance(outer, left, right));
            }
        }

        public void Add(IScoped outer, ParseTree.RequirementClause tree)
        {
            foreach (var req in tree.Requirements)
            {
                Add(Build(outer, req));
            }
        }

        private GenericRestriction Build(IScoped outer, ParseTree.ITypeRestriction tree)
        {
            return Visit(outer, (dynamic)tree);
        }

        private GenericRestriction Visit(IScoped outer, ParseTree.ITypeRestriction.SameTypeRequirement tree)
        {
            var left = ITypeDefinition.Build(outer, tree.Name);
            var right = ITypeDefinition.Build(outer, tree.Type);
            return new GenericSameType(outer, left, right);
        }

        private GenericRestriction Visit(IScoped outer, ParseTree.ITypeRestriction.ConformanceRequirement tree)
        {
            var left = ITypeDefinition.Build(outer, tree.Name);
            var right = ITypeDefinition.Build(outer, tree.Composition);
            return new GenericConformance(outer, left, right);
        }

        private GenericRestriction Visit(IScoped outer, ParseTree.ITypeRestriction tree)
        {
            throw new NotImplementedException();
        }
    }
}
