using SixComp.Support;

namespace SixComp.Sema
{
    public class GenericArgument : Base, IType
    {
        public GenericArgument(IScoped outer, Tree.GenericArgument tree)
            : base(outer)
        {
            Tree = tree;

            Type = IType.Build(Outer, Tree.Type);
        }

        public Tree.GenericArgument Tree { get; }

        public IType Type { get; }

        public override void Report(IWriter writer)
        {
            Type.Report(writer);
        }
    }
}
