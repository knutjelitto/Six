using Six.Support;

namespace SixComp.Sema
{
    public class ArrayType : Base<ParseTree.ArrayType>, ITypeDefinition
    {
        public ArrayType(IScoped outer, ParseTree.ArrayType tree)
            : base(outer, tree)
        {
            Element = ITypeDefinition.Build(Outer, Tree.ElementType);
        }

        public ITypeDefinition Element { get; }

        public override void Report(IWriter writer)
        {
            Element.Report(writer, Strings.Head.ArrayType);
        }
    }
}
