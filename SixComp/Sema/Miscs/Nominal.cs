using SixComp.Entities;
using System.Collections.Generic;

namespace SixComp.Sema
{
    public abstract class Nominal<TTree> : BaseScoped<TTree>, INamedDeclaration, IWithGenerics, IExtendable
    {
        private readonly List<ExtensionDeclaration> extensions = new List<ExtensionDeclaration>();

        public Nominal(IScoped outer, TTree tree, Tree.BaseName name)
            : base(outer, tree)
        {
            Name = new BaseName(outer, name);
            Where = new GenericRestrictions(this);
        }

        public BaseName Name { get; }
        public GenericRestrictions Where { get; }
        public abstract GenericParameters Generics { get; }
        public abstract Declarations Declarations { get; }

        public IReadOnlyList<ExtensionDeclaration> Extensions => extensions;

        public virtual bool Extend(ExtensionDeclaration extension)
        {
            extensions.Add(extension);
            return true;
        }
    }
}
