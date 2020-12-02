using SixComp.Support;

namespace SixComp.Sema
{
    public class TupleTypeElement : Base, IType
    {
        public TupleTypeElement(IScoped outer, Tree.TupleTypeElement tree)
            : base(outer)
        {
            Tree = tree;

            Label = Tree.Label?.ToString();
            Type = IType.Build(Outer, Tree.Type);
        }

        public string? Label { get; }
        public IType Type { get; }

        public Tree.TupleTypeElement Tree { get; }

        public override void Report(IWriter writer)
        {
            writer.Write($"{Label.SpEnd()}");
            Type.Report(writer);
        }
    }
}
