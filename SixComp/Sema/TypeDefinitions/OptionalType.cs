using SixComp.Support;

namespace SixComp.Sema
{
    public class OptionalType : Base, ITypeDefinition
    {
        public OptionalType(IScoped outer, Tree.OptionalType tree)
            : base(outer)
        {
            Tree = tree;

            Type = ITypeDefinition.Build(Outer, Tree.Type);
        }

        public Tree.OptionalType Tree { get; }
        public ITypeDefinition Type { get; }

        public override void Report(IWriter writer)
        {
            Type.Report(writer, Strings.Head.Optional);
        }
    }
}
