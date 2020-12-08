using SixComp.Support;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SixComp.Sema
{
    public class CompositionType : Items<ITypeDefinition, Tree.ProtocolCompositionType>, ITypeDefinition
    {
        public CompositionType(IScoped outer, Tree.ProtocolCompositionType tree)
            : base(outer, tree, Enum(outer, tree))
        {
            Debug.Assert(tree.Count >= 2);
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.Union);
        }

        private static IEnumerable<ITypeDefinition> Enum(IScoped outer, Tree.ProtocolCompositionType tree)
        {
            return tree.Select(type => ITypeDefinition.Build(outer, type));
        }
    }
}
