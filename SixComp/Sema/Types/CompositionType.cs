using SixComp.Support;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SixComp.Sema
{
    public class CompositionType : Items<IType>, IType
    {
        public CompositionType(IScoped outer, Tree.ProtocolCompositionType tree)
            : base(outer, Enum(outer, tree))
        {
            Debug.Assert(tree.Count >= 2);

            Tree = tree;
        }
        public Tree.ProtocolCompositionType Tree { get; }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.Union);
        }

        private static IEnumerable<IType> Enum(IScoped outer, Tree.ProtocolCompositionType tree)
        {
            return tree.Select(type => IType.Build(outer, type));
        }
    }
}
