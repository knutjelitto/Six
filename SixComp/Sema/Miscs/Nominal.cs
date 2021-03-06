﻿using SixComp.Entities;

namespace SixComp.Sema
{
    public abstract class Nominal<TTree> : BaseScoped<TTree>, INamedDeclaration, IWithGenerics
        where TTree : ParseTree.INominal
    {
        public Nominal(IScoped outer, TTree tree)
            : base(outer, tree)
        {
            Name = new BaseName(outer, Tree.Name);
            Where = new GenericRestrictions(this);
            Generics = new GenericParameters(this, Tree.Generics);
            Where.Add(this, Tree.Requirements);
            if (Tree is ParseTree.INominalWhithDeclarations with)
            {
                Inheritance = new Inheritance(Outer, with.Inheritance);
                Declarations = new Declarations(this, with.Declarations);
            }
            else
            {
                Inheritance = new Inheritance(Outer);
                Declarations = new Declarations(this);
            }
        }

        public BaseName Name { get; }
        public GenericRestrictions Where { get; }
        public GenericParameters Generics { get; }
        public Inheritance Inheritance { get; }
        public Declarations Declarations { get; }
    }
}
