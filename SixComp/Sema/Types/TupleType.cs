using SixComp.Support;
using System.Collections.Generic;
using System.Linq;

namespace SixComp.Sema
{
    public class TupleType : Items<TupleType.Element, Tree.TupleType>, ITypeDefinition
    {
        public TupleType(IScoped outer, Tree.TupleType tree)
            : base(outer, tree, Enum(outer, tree))
        {
        }

        public override void Resolve(IWriter writer)
        {
            ResolveItems(writer);
        }

        public override void Report(IWriter writer)
        {
            this.ReportList(writer, Strings.Head.TupleType);
        }

        public override string ToString()
        {
            if (Count == 1)
            {
                return this[0].ToString()!;
            }
            return base.ToString()!;
        }

        private static IEnumerable<Element> Enum(IScoped outer, Tree.TupleType tree)
        {
            return tree.Elements.Select(element => new Element(outer, element));
        }

        public class Element : Base<Tree.TupleTypeElement>, ITypeDefinition
        {
            public Element(IScoped outer, Tree.TupleTypeElement tree)
                : base(outer, tree)
            {
                Label = BaseName.Maybe(outer, Tree.Label);
                Type = ITypeDefinition.Build(Outer, Tree.Type);
            }

            public BaseName? Label { get; }
            public ITypeDefinition Type { get; }

            public override void Resolve(IWriter writer)
            {
                Resolve(writer, Type);
            }

            public override void Report(IWriter writer)
            {
                Label.Report(writer, Strings.Head.Label);
                Type.Report(writer, Strings.Head.Type);
            }

            public override string ToString()
            {
                if (Label == null)
                {
                    var text = Type.ToString()!;
                    if (!text.StartsWith("SicComp."))
                    {
                        return text;
                    }
                }
                return base.ToString()!;
            }
        }

    }
}
