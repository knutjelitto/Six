using SixComp.Support;

namespace SixComp.Sema
{
    public class ArrayType : Base, IType
    {
        public ArrayType(IScoped outer, Tree.ArrayType tree)
            : base(outer)
        {
            Tree = tree;
            Element = IType.Build(Outer, Tree.ElementType);
        }

        public Tree.ArrayType Tree { get; }

        public IType Element { get; }

        public override void Report(IWriter writer)
        {
            writer.WriteLine(Strings.ArrayType);
            writer.Indent(() => Element.Report(writer));
        }
    }
}
