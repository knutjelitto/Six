using Six.Support;
using SixComp.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class Scope : IReportable
    {
        private static readonly IReadOnlyList<IEntity> NoEntity = new IEntity[] { };
        private readonly Dictionary<BaseName, (int order, List<IEntity> list)> lookup = new Dictionary<BaseName, (int order, List<IEntity> list)>();
        private readonly List<ExtensionDeclaration> Extensions = new List<ExtensionDeclaration>();

        public Scope(IScoped outer, Module? module = null)
        {
            Outer = outer;
            Module = module ?? outer.Scope.Module;
        }

        public Module Module { get; }
        public IScoped Outer { get; }

        public Global Global => Module.Global;
            
        public void Declare(IEntity named)
        {
            if (!lookup.TryGetValue(named.Name, out var list))
            {
                list = (lookup.Count, new List<IEntity>());
                lookup.Add(named.Name, list);
            }

            list.list.Add(named);
        }

        public bool Extend(ExtensionDeclaration with)
        {
            Extensions.Add(with);
            return true;
        }

        public IReadOnlyList<IEntity> Look(INamed named, bool stop = false)
        {
            var found = new List<IEntity>();

            if (lookup.TryGetValue(named.Name, out var decls))
            {
                found.AddRange(decls.list);
            }
            foreach (var extension in Extensions)
            {
                found.AddRange(extension.Scope.Look(named));
            }
            return found;
        }

        private static IReadOnlyList<IEntity> LookUp(Scope scope, INamed named)
        {
            var found = new List<IEntity>();
            while (true)
            {
                found.AddRange(scope.Look(named));
                if (scope == scope.Outer.Scope)
                {
                    break;
                }
                scope = scope.Outer.Scope;
            }
            return found;
        }

        public IReadOnlyList<IEntity> LookUp(INamed named)
        {
            var found = LookUp(this, named);

            return found;
        }

        public T FindParent<T>(IScoped scoped) where T : notnull, IScoped
        {
            while (!(scoped is T))
            {
                if (scoped is Module)
                {
                    throw new System.NotImplementedException();
                }
                scoped = scoped.Outer;
            }

            return (T)scoped;
        }

        public void Report(IWriter writer)
        {
            foreach (var decls in lookup.Values.OrderBy(l => l.order).Select(l => l.list))
            {
                foreach (var decl in decls)
                {
                    writer.WriteLine($"{decl.Name}");
                    if (decl.IsParentScope)
                    {
                        using (writer.Indent())
                        {
                            decl.Scope.Report(writer);
                        }
                    }
                }
            }
        }

        public override string ToString()
        {
            // this.FindParent(this);
            return "XXX";
        }
    }
}
