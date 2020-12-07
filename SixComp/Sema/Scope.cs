using SixComp.Entities;
using SixComp.Support;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SixComp.Sema
{
    public class Scope : IScope, IReportable
    {
        private readonly Dictionary<BaseName, (int order, List<IEntity> list)> lookup = new Dictionary<BaseName, (int order, List<IEntity> list)>();
        private readonly List<ExtensionDeclaration> Extensions = new List<ExtensionDeclaration>();

        public Scope(IScoped parent, Module? module = null)
        {
            Parent = parent;
            Module = module ?? parent.Scope.Module;
        }

        public Module Module { get; }
        public IScoped Parent { get; }
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

        public void Extend(ExtensionDeclaration extension)
        {
            Extensions.Add(extension);
        }

        public IReadOnlyList<IEntity> Look(INamed named)
        {
            if (lookup.TryGetValue(named.Name, out var decls))
            {
                return decls.list;
            }
            var found = new List<IEntity>();
            foreach (var extension in Extensions)
            {
                found.AddRange(extension.Scope.Look(named));
            }
            if (found.Count > 0)
            {
                return found;
            }
            return IScope.NoEntity;
        }

        public IReadOnlyList<IEntity> LookUp(INamed named)
        {
            Scope scope = this;
            while (true)
            {
                if (scope.lookup.TryGetValue(named.Name, out var decls))
                {
                    return decls.list;
                }
                if (scope == Module.Scope)
                {
                    break;
                }
                scope = (Scope)scope.Parent.Scope;
            }
            return IScope.NoEntity;
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
    }
}
