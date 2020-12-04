using SixComp.Support;

namespace SixComp.Sema
{
    public class ArrayType : Base<Tree.ArrayType>, IType
    {
        public ArrayType(IScoped outer, Tree.ArrayType tree)
            : base(outer, tree)
        {
            Element = IType.Build(Outer, Tree.ElementType);
        }

        public IType Element { get; }

        public override void Report(IWriter writer)
        {
            Element.Report(writer, Strings.Head.ArrayType);
        }
    }
}
