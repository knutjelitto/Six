using SixComp.Support;

namespace SixComp.Sema
{
    public class OptionalType : Base, IType
    {
        public OptionalType(IScoped outer, Tree.OptionalType tree)
            : base(outer)
        {
            Tree = tree;

            Type = IType.Build(Outer, Tree.Type);
        }

        public Tree.OptionalType Tree { get; }
        public IType Type { get; }

        public override void Report(IWriter writer)
        {
            Type.Report(writer, Strings.Head.Optional);
        }
    }
}
