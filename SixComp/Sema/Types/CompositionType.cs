using SixComp.Support;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SixComp.Sema
{
    public class CompositionType : Items<ITypeDefinition, ParseTree.ProtocolCompositionType>, ITypeDefinition
    {
        public CompositionType(IScoped outer, ParseTree.ProtocolCompositionType tree)
            : base(outer, tree, Enum(outer, tree))
        {
            Debug.Assert(tree.Count >= 2);
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.Union);
        }

        private static IEnumerable<ITypeDefinition> Enum(IScoped outer, ParseTree.ProtocolCompositionType tree)
        {
            return tree.Select(type => ITypeDefinition.Build(outer, type));
        }
    }
}
