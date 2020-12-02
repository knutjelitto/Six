using System;
using System.Collections.Generic;

namespace SixComp.Sema
{
    public class Scope : IScope
    {
        private readonly Dictionary<BaseName, List<INamed>> lookup = new Dictionary<BaseName, List<INamed>>();

        public Scope(IScoped parent, Package? package = null)
        {
            Parent = parent;
            Package = package ?? parent.Scope.Package;
        }

        public Package Package { get; }
        public IScoped Parent { get; }

        public void Add(INamed named)
        {
            if (!lookup.TryGetValue(named.Name, out var list))
            {
                list = new List<INamed>();
                lookup.Add(named.Name, list);
            }

            list.Add(named);
        }

        public void AddUnique(INamed named)
        {
            throw new NotImplementedException();
        }
    }
}
