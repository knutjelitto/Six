using SixComp.Support;

namespace SixComp.Sema
{
    public class GenericArgument : Base, ITypeDefinition
    {
        public GenericArgument(IScoped outer, Tree.GenericArgument tree)
            : base(outer)
        {
            Tree = tree;

            Type = ITypeDefinition.Build(Outer, Tree.Type);
        }

        public Tree.GenericArgument Tree { get; }

        public ITypeDefinition Type { get; }

        public override void Report(IWriter writer)
        {
            Type.Report(writer);
        }
    }
}
