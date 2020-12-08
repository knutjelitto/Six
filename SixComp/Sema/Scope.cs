using SixComp.Entities;
using SixComp.Support;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SixComp.Sema
{
    public class Scope : IScope, IReportable
    {
        private readonly Dictionary<BaseName, (int order, List<IEntity> list)> lookup = new Dictionary<BaseName, (int order, List<IEntity> list)>();
        private readonly List<ExtensionDeclaration> Extensions = new List<ExtensionDeclaration>();
        private IEntity? Extendee = null;

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

        public void Extends(IEntity extendee)
        {
            Extendee = extendee;
        }

        public IReadOnlyList<IEntity> Look(INamed named, bool stop = false)
        {
            if (named.Name.Text == "Base")
            {
                Debug.Assert(true);
            }
            if (lookup.TryGetValue(named.Name, out var decls))
            {
                return decls.list;
            }
            var found = new List<IEntity>();
            if (!stop)
            {
                foreach (var extension in Extensions)
                {
                    found.AddRange(extension.Scope.Look(named));
                }
                if (found.Count == 0 && Extendee != null)
                {
                    found.AddRange(Extendee.Scope.Look(named, true));
                }
            }
            return found;
        }

        public IReadOnlyList<IEntity> LookUp(INamed named)
        {
            Scope scope = this;
            while (true)
            {
                var found = scope.Look(named);
                if (found.Count > 0)
                {
                    return found;
                }
                if (scope == Module.Scope)
                {
                    return IScope.NoEntity;
                }
                scope = (Scope)scope.Parent.Scope;
            }
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
