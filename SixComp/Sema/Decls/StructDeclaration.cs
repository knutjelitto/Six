﻿using SixComp.Support;

namespace SixComp.Sema
{
    public class StructDeclaration : BaseScoped<Tree.StructDeclaration>, INamedDeclaration, IWhere
    {
        public StructDeclaration(IScoped outer, Tree.StructDeclaration tree)
            : base(outer, tree)
        {
            Name = new BaseName(Outer, Tree.Name);
            Where = new GenericRestrictions(this);
            GenericParameters = new GenericParameters(this, Tree.Generics);
            Inheritance = new Inheritance(this, Tree.Inheritance);
            Where.Add(this, Tree.Requirements);
            Declarations = new Declarations(this, Tree.Declarations);

            Declare(this);
        }

        public BaseName Name { get; }
        public GenericParameters GenericParameters { get; }
        public Inheritance Inheritance { get; }
        public GenericRestrictions Where { get; }
        public Declarations Declarations { get; }

        public override void Resolve(IWriter writer)
        {
            Resolve(writer, GenericParameters, Inheritance, Where, Declarations);
        }

        public override void Report(IWriter writer)
        {
            Tree.Tree(writer);
            using (writer.Indent(Strings.Head.Struct))
            {
                Name.Report(writer, Strings.Head.Name);
                GenericParameters.Report(writer);
                Inheritance.Report(writer);
                Where.Report(writer);
                Declarations.Report(writer);
            }
        }
    }
}
