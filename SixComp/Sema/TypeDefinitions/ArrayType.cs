using SixComp.Support;

namespace SixComp.Sema
{
    public class ArrayType : Base<Tree.ArrayType>, ITypeDefinition
    {
        public ArrayType(IScoped outer, Tree.ArrayType tree)
            : base(outer, tree)
        {
            Element = ITypeDefinition.Build(Outer, Tree.ElementType);
        }

        public ITypeDefinition Element { get; }

        public override void Resolve(IWriter writer)
        {
            Resolve(writer, Element);
        }

        public override void Report(IWriter writer)
        {
            Element.Report(writer, Strings.Head.ArrayType);
        }
    }
}
