using Six.Support;

namespace SixComp.Sema
{
    public class GenericArgument : Base<ParseTree.GenericArgument>, ITypeDefinition
    {
        public GenericArgument(IScoped outer, ParseTree.GenericArgument tree)
            : base(outer, tree)
        {
            Type = ITypeDefinition.Build(Outer, Tree.Type);
        }

        public ITypeDefinition Type { get; }

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
