using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class Scope : IScope, IReportable
    {
        private readonly Dictionary<BaseName, (int order, List<INamedDeclaration> list)> lookup = new Dictionary<BaseName, (int order, List<INamedDeclaration> list)>();

        public Scope(IScoped parent, Module? module = null)
        {
            Parent = parent;
            Module = module ?? parent.Scope.Module;
        }

        public Module Module { get; }
        public IScoped Parent { get; }
        public Global Global => Module.Global;

        public string Name
        {
            get
            {
                if (Parent.Scope == this)
                {
                    return Module.ModuleName;
                }
                else
                {
                    return $"{((Scope)Parent.Scope).Name}.<?>";
                }
            }
        }

        public void Declare(INamedDeclaration named)
        {
            if (!lookup.TryGetValue(named.Name, out var list))
            {
                list = (lookup.Count, new List<INamedDeclaration>());
                lookup.Add(named.Name, list);
            }

            list.list.Add(named);
        }

        public void Report(IWriter writer)
        {
            using (writer.Indent($"scope {Name}"))
            {
                foreach (var decls in lookup.Values.OrderBy(l => l.order).Select(l => l.list))
                {
                    writer.WriteLine($"{decls.First().Name}");
                    using (writer.Indent())
                    {
                        foreach (var decl in decls)
                        {
                            writer.WriteLine($"{decl}");
                        }
                    }
                }
            }
        }
    }
}
