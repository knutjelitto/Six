using SixComp.Support;

namespace SixComp.Sema
{
    public class GenericArgument : Base<Tree.GenericArgument>, ITypeDefinition
    {
        public GenericArgument(IScoped outer, Tree.GenericArgument tree)
            : base(outer, tree)
        {
            Type = ITypeDefinition.Build(Outer, Tree.Type);
        }

        public ITypeDefinition Type { get; }

        public override void Resolve(IWriter writer)
        {
            Resolve(writer, Type);
        }
        public override void Report(IWriter writer)
        {
            Type.Report(writer);
        }

        public override string ToString()
        {
            return $"{Type}";
        }
    }
}
