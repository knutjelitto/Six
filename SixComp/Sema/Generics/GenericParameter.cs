﻿using SixComp.Entities;
using SixComp.Support;

namespace SixComp.Sema
{
    public class GenericParameter : Base<Tree.GenericParameter>, INamedDeclaration
    {
        public GenericParameter(IWithRestrictions where, Tree.GenericParameter tree)
            : base(where, tree)
        {
            Name = new BaseName(Outer, Tree.Name);

            if (Tree.Requirement != null)
            {
                where.Where.Add(Outer, Tree);
            }

            Declare(this);
        }

        public BaseName Name { get; }

        public override void Resolve(IWriter writer)
        {
            // nothing to resolve
            // TODO: remove thee following
            //Debug.Assert(Scope.Look(this).Single() == this);
        }

        public override void Report(IWriter writer)
        {
            writer.WriteLine(Name.Text);
        }

        public override string ToString()
        {
            return Name.Text;
        }
    }
}
