using SixComp.Support;

namespace SixComp.Sema
{
    public class TupleTypeElement : Base<Tree.TupleTypeElement>, IType
    {
        public TupleTypeElement(IScoped outer, Tree.TupleTypeElement tree)
            : base(outer, tree)
        {
            Label = BaseName.Maybe(outer, Tree.Label);
            Type = IType.Build(Outer, Tree.Type);
        }

        public BaseName? Label { get; }
        public IType Type { get; }

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
